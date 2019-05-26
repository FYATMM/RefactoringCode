using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        public PatientHistoryXMLStorage PatientHistoryXmlStorage { get; private set; }

        public CaloriesCalculator()
        {
            InitializeComponent();
            PatientHistoryXmlStorage = new PatientHistoryXMLStorage();
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
                MessageBox.Show("You must enter Patient's first name.  ");
                txtFirstName.Select();
                return false;
            }

            if (txtLastName.Text.Trim().Length < 1)
            {
                MessageBox.Show("You must enter Patient's last name.  ");
                txtFirstName.Select();
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if(!ValidatePatientPersonalData()  |  UserInputInvalid()) return;
           //确保在保存之前完成了计算，通过调用计算按键事件，即保存前先计算，防止忘点
           btnCalculate_Click(null,null);
           PatientHistoryXmlStorage.Save(Patient);
        }

        //调用系统程序，及要打开的文件路径
        private void btnView_Click_1(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe",
                PatientHistoryXmlStorage.PatientHistoryFileLocation);
        }
    }
}
