using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Engine;

namespace CaloriesCalculator
{
    public partial class CaloriesCalculator : Form
    {
        public Patient Patient { get; set; }

        public CaloriesCalculator()
        {
            InitializeComponent();
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            ClearResults();

            if (UserInputInvalid()) return;

            //Patient = new Patient
            //{
            //    HeightInInches = Convert.ToDouble(txtFeet.Text) * 12 + Convert.ToDouble(txtInches.Text),
            //    WeightInPounds = Convert.ToDouble(txtWeight.Text),
            //    Age = Convert.ToDouble(txtAge.Text),
            //    Gender = rbtnMale.Checked ? Gender.Male : Gender.Female
            //};//使用时再初始化，初始化时赋值

            if (rbtnFemale.Checked) Patient = new FemalePaient();
            if (rbtnMale.Checked) Patient = new MalePatient();

            Patient.HeightInInches = Convert.ToDouble(txtFeet.Text) * 12 + Convert.ToDouble(txtInches.Text);
            Patient.WeightInPounds = Convert.ToDouble(txtWeight.Text);
            Patient.Age = Convert.ToDouble(txtAge.Text);
 
            txtCalories.Text = Patient.DailyCaloriesRecommended().ToString();
            txtIdealWeight.Text = Patient.IdealBodyWeight().ToString();
            txtDistance.Text = Patient.DistanceFromIdealWeight().ToString();
        }



        private bool UserInputInvalid()
        {
            /*
             * v1.1需要加入用户输入数据验证，防止出错退出
             * 通过tryparse的结果来判断
             */
            //验证身高的两个输入框
            double result;

            if (!double.TryParse(txtFeet.Text, out result))
            {
                MessageBox.Show("必须输入数字。");
                txtFeet.Select();
                return true;
            }

            if (!double.TryParse(txtInches.Text, out result))
            {
                MessageBox.Show("必须输入数字。");
                txtInches.Select();
                return true;
            }

            //验证重量
            if (!double.TryParse(txtWeight.Text, out result))
            {
                MessageBox.Show("必须输入数字。");
                txtWeight.Select();
                return true;
            }

            //验证年龄
            if (!double.TryParse(txtAge.Text, out result))
            {
                MessageBox.Show("必须输入数字。");
                txtAge.Select();
                return true;
            }

            /*
             * v1.2 判断身高必须》5
             */
            if (!(Convert.ToDouble(txtFeet.Text) >= 5))
            {
                MessageBox.Show("身高必去大于等于5");
                txtFeet.Select();
                return true;
            }

            return false;
        }

        private void ClearResults()
        {
            /*
             *v1.2 需求开始增加
             *增加一个计算理想体重的功能，有一个公式计算及显示，还需要身高》5
             * 并且需要在每次计算前清楚旧的结果，即使输入错误，也要先清楚旧结果
             */
            txtDistance.Text = "";
            txtIdealWeight.Text = "";
            txtCalories.Text = "";
        }

        private bool ValidatePatientPersonalData()
        {
            int result;
            if (
                (!int.TryParse(txtSSNFirstPart.Text, out result)) |
                (!int.TryParse(txtSSNSecondPart.Text, out result)) |
                (!int.TryParse(txtSSNThirdPart.Text, out result))
            )
            {
                MessageBox.Show("You must enter valid SSN.");
                txtSSNFirstPart.Select();
                return false;
            }

            if (txtFirstName.Text.Trim().Length < 1)
            {
                MessageBox.Show("You must enter patient's first name.  ");
                txtFirstName.Select();
                return false;
            }

            if (txtLastName.Text.Trim().Length < 1)
            {
                MessageBox.Show("You must enter patient's last name.  ");
                txtFirstName.Select();
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if(!ValidatePatientPersonalData()  |  !UserInputInvalid()) return;
           //确保在保存之前完成了计算，通过调用计算按键事件，即保存前先计算，防止忘点
           btnCalculate_Click(null,null);
           bool fileCreated = true;
           XmlDocument document = new XmlDocument();
           try
           {
               //获取当前执行程序的文件夹，并替换路径为xml文件
                document.Load(Assembly.GetExecutingAssembly().Location
                    .Replace("CaloriesCalculator.exe", "PatientHistory.xml"));
           }
           catch (FileNotFoundException fileNotFoundException)
           {
               //Console.WriteLine(fileNotFoundException);
               //throw;
               fileCreated = false;
           }

           if (!fileCreated)
           {
               document.LoadXml(
                   "<PatientHistory>" + 
                        @"<patient ssn = \" + Patient.SSN + @"\" +
                        @"<firstName=\" + Patient.FirstName + @"\" +
                        @"<lastName=\"  + Patient.LastName + @"\" + ">" +
                            @"<measurement date=\" + DateTime.Now + @"\" + ">" +
                                "<height>" + Patient.HeightInInches + "</height>" + 
                                "<weight>" + Patient.WeightInPounds + "</weight>" +
                                "<age>" + Patient.Age + "</age>" + 
                                "<dailyCaloriesRecommended>" + Patient.DailyCaloriesRecommended() + "</dailyCaloriesRecommended>" + 
                                "<idealBodyWeight>" + Patient.IdealBodyWeight() + "</idealBodyWeight>" + 
                                "<distanceFromIdealWeight>" + Patient.DistanceFromIdealWeight() + "</distanceFromIdealWeight>" +
                            @"</measurement>" +
                        @"</patient>" +
                   "</PatientHistory>"
                   );
           }
           else
           {
               //搜索存在的patient节点
               XmlNode patientNode = null;
               foreach (XmlNode node in document.FirstChild.ChildNodes)
               {
                   foreach (XmlAttribute attribute in node.Attributes)
                   {
                       //用SSN作唯一标识
                       if ((attribute.Name == "ssn") & (attribute.Value == Patient.SSN))
                       {
                           patientNode = node;
                       }
                   }
               }

               if (patientNode == null)
               {
                   //如果没有，克隆任意病人节点用来给新的病人存储信息
                   XmlNode thisPatient = document.DocumentElement.FirstChild.CloneNode(false);
                   thisPatient.Attributes["ssn "].Value = Patient.SSN;
                   thisPatient.Attributes["firstName"].Value = Patient.FirstName;
                   thisPatient.Attributes["lastName"].Value = Patient.LastName;

                   XmlNode measurement = document.DocumentElement.FirstChild["measurement"].CloneNode(true);
                   SetMeasurementValues(measurement);

                    thisPatient.AppendChild(measurement);
                   document.FirstChild.AppendChild(thisPatient);
               }
               else
               {
                   //如果找到节点，克隆一个节点，再保存信息
                   XmlNode measurement = patientNode.FirstChild.CloneNode(true);
                    SetMeasurementValues(measurement);

                    patientNode.AppendChild(measurement);
               }
               //保存xml
               document.Save(Assembly.GetExecutingAssembly().Location.Replace("CaloriesCalculatior.exe ","PatientsHistory.xml"));
           }
        }

        private void SetMeasurementValues(XmlNode measurement)
        {
            measurement.Attributes["date"].Value = DateTime.Now.ToString();
            measurement["height"].FirstChild.Value = Patient.HeightInInches.ToString();
            measurement["weight"].FirstChild.Value = Patient.WeightInPounds.ToString();
            measurement["age"].FirstChild.Value = Patient.Age.ToString();
            measurement["dailyCaloriesRecommended"].FirstChild.Value = Patient.DailyCaloriesRecommended().ToString();
            measurement["idealBodyWeight"].FirstChild.Value = Patient.IdealBodyWeight().ToString();
            measurement["distanceFromIdealWeight"].FirstChild.Value = Patient.DistanceFromIdealWeight().ToString();
        }
    }
}
