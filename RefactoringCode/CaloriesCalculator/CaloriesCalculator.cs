using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace CaloriesCalculator
{
    public partial class CaloriesCalculator : Form
    {
        private readonly Patient _patient;

        public CaloriesCalculator()
        {
            InitializeComponent();
            _patient = new Patient();
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            ClearResults();

            if (UserInputInvalid()) return;

            if (rbtnMale.Checked)
            {
                txtCalories.Text = _patient.DailyCaloriesRecommended().ToString();
                //txtCalories.Text = Patient.DailyCaloriesRecommendedMale(Convert.ToDouble(txtWeight.Text), ((Convert.ToDouble(txtFeet.Text) * 12)
                //                                                                                   + Convert.ToDouble(txtInches.Text)), Convert.ToDouble(txtAge.Text)).ToString();
                txtIdealWeight.Text = _patient.IdealBodyWeight().ToString();
                //txtIdealWeight.Text = Patient.IdealBodyWeightMale((((Convert.ToDouble(txtFeet.Text) - 5) * 12)
                //                                           + Convert.ToDouble(txtInches.Text))).ToString();
            }
            else
            {
                txtCalories.Text = _patient.DailyCaloriesRecommended().ToString();
                //txtCalories.Text = Patient.DailyCaloriesRecommendedFemale(Convert.ToDouble(txtWeight.Text), ((Convert.ToDouble(txtFeet.Text) * 12)
                //                                                                                     + Convert.ToDouble(txtInches.Text)), Convert.ToDouble(txtAge.Text)).ToString();
                txtIdealWeight.Text = _patient.IdealBodyWeight().ToString();
                //txtIdealWeight.Text = Patient.idealBodyWeightFemale((((Convert.ToDouble(txtFeet.Text) - 5) * 12)
                //                                             + Convert.ToDouble(txtInches.Text))).ToString();
            }
            //txtDistance.Text = DistanceFromIdealWeight(Convert.ToDouble(txtWeight.Text), Convert.ToDouble(txtIdealWeight.Text)).ToString();
            txtDistance.Text = _patient.DistanceFromIdealWeight().ToString();
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
    }
}
