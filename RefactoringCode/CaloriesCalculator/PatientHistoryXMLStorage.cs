using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Engine;

namespace CaloriesCalculator
{
    public class PatientHistoryXMLStorage
    {
        public Patient Patient { get; set; }
        public XmlDocument Document { get; set; }
        public string PatientHistoryFileLocation
        {
            get
            {
                return System.Reflection.Assembly.
                    GetExecutingAssembly().Location.Replace(
                        "CaloriesCalculator.exe", "PatientsHistory.xml");
            }
            private set { }
        }

        public PatientHistoryXMLStorage()
        {
            Document = new XmlDocument();
        }
        //搜索存在的patient节点
        public void AddNewPatient()
        {
            XmlNode thisPatient = Document.DocumentElement.FirstChild.CloneNode(false);
            thisPatient.Attributes["ssn"].Value = Patient.SSN;
            thisPatient.Attributes["firstName"].Value = Patient.FirstName;
            thisPatient.Attributes["lastName"].Value = Patient.LastName;

            XmlNode measurement = Document.DocumentElement.FirstChild["measurement"].CloneNode(true);
            SetMeasurementValues(measurement);

            thisPatient.AppendChild(measurement);
            Document.FirstChild.AppendChild(thisPatient);
        }

        public XmlNode FindPatientNode()
        {
            XmlNode patientNode = null;
            foreach (XmlNode node in Document.FirstChild.ChildNodes)
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

            return patientNode;
        }

        public void CreatePatientHistoryXmlFirstTime()
        {
            //XmlDocument Document  = new XmlDocument();
            Document.LoadXml(
                 //"<PatientHistory>" + 
                 //            "<Patient ssn=\"" + Patient.SSN + "\"" + "firstName=\"" + Patient.FirstName + "\"" + "lastName=\"" + Patient.LastName + "\"" + ">" +
                 //            "<measurement date=\"" + DateTime.Now + "\"" + ">" +
                 //            "<height>" + Patient.HeightInInches + "</height>" +
                 //            "<weight>" + Patient.WeightInPounds + "</weight>" +
                 //            "<age>" + Patient.Age + "</age>" +
                 //            "<dailyCaloriesRecommended>" + Patient.DailyCaloriesRecommended() + "</dailyCaloriesRecommended>" +
                 //            "<idealBodyWeight>" + Patient.IdealBodyWeight() + "</idealBodyWeight>" +
                 //            "<distanceFromIdealWeight>" + Patient.DistanceFromIdealWeight() + "</distanceFromIdealWeight>" +
                 //            "</measurement>" +
                 //            "</Patient>" +
                 //            "</PatientHistory>");
                 "<PatientsHistory>" +
                 "<Patient ssn=\"" + Patient.SSN + "\"" +
                 " firstName=\"" + Patient.FirstName + "\"" +
                 " lastName=\"" + Patient.LastName + "\"" + ">" +
                 "<measurement date=\"" + DateTime.Now + "\"" + ">" +
                 "<height>" + Patient.HeightInInches + "</height>" +
                 "<weight>" + Patient.WeightInPounds + "</weight>" +
                 "<age>" + Patient.Age + "</age>" +
                 "<dailyCaloriesRecommended>" +
                 Patient.DailyCaloriesRecommended() +
                 "</dailyCaloriesRecommended>" +
                 "<idealBodyWeight>" +
                 Patient.IdealBodyWeight() +
                 "</idealBodyWeight>" +
                 "<distanceFromIdealWeight>" +
                 Patient.DistanceFromIdealWeight() +
                 "</distanceFromIdealWeight>" +
                 "</measurement>" +
                 "</Patient>" +
                 "</PatientsHistory>");
        }

        public void LoadHistoryFile()
        {
            ////XmlDocument Document = new XmlDocument();
            //获取当前执行程序的文件夹，并替换路径为xml文件
            Document.Load(PatientHistoryFileLocation);
        }

        public XmlNode SetMeasurementValues(XmlNode measurement)
        {
            measurement.Attributes["date"].Value = DateTime.Now.ToString();
            measurement["height"].FirstChild.Value = Patient.HeightInInches.ToString();
            measurement["weight"].FirstChild.Value = Patient.WeightInPounds.ToString();
            measurement["age"].FirstChild.Value = Patient.Age.ToString();
            measurement["dailyCaloriesRecommended"].FirstChild.Value = Patient.DailyCaloriesRecommended().ToString();
            measurement["idealBodyWeight"].FirstChild.Value = Patient.IdealBodyWeight().ToString();
            measurement["distanceFromIdealWeight"].FirstChild.Value = Patient.DistanceFromIdealWeight().ToString();
            return measurement;
        }

        public void Save(Patient patient)
        {
            bool fileCreated = true;
            Patient = patient;
            try
            {
                LoadHistoryFile();
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                //Console.WriteLine(fileNotFoundException);
                //throw;
                fileCreated = false;
            }
            //catch (Exception ex)
            //{
            //    fileCreated = false;
            //    MessageBox.Show(ex.Message);
            //}

            if (!fileCreated)
            {
                CreatePatientHistoryXmlFirstTime();
            }
            else
            {
                XmlNode patientNode = FindPatientNode();

                if (patientNode == null)
                {
                    //如果没有，克隆任意病人节点用来给新的病人存储信息
                    this.AddNewPatient();
                }
                else
                {
                    //如果找到节点，克隆一个节点，再保存信息
                    XmlNode measurement = patientNode.FirstChild.CloneNode(true);
                    measurement = SetMeasurementValues(measurement);
                    patientNode.AppendChild(measurement);
                }
            }
            //保存xml
            Document.Save(PatientHistoryFileLocation);
        }
    }
}