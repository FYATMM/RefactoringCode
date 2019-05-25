using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaloriesCalculator
{
    public partial class CaloriesCalculator : Form
    {
        public CaloriesCalculator()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            /*
             *v1.2 需求开始增加
             *增加一个计算理想体重的功能，有一个公式计算及显示，还需要身高》5
             * 并且需要在每次计算前清楚旧的结果，即使输入错误，也要先清楚旧结果
             */
            txtDistance.Text = "";
            txtIdealWeight.Text = "";
            txtCalories.Text = "";

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
                 return;
             }
            if (!double.TryParse(txtInches.Text, out result))
            {
                MessageBox.Show("必须输入数字。");
                txtInches.Select();
                return;
            }
            //验证重量
            if (!double.TryParse(txtWeight.Text, out result))
            {
                MessageBox.Show("必须输入数字。");
                txtWeight.Select();
                return;
            }

            //验证年龄
            if (!double.TryParse(txtAge.Text, out result))
            {
                MessageBox.Show("必须输入数字。");
                txtAge.Select();
                return;
            }

            /*
             * v1.2 判断身高必须》5
             */
            if (!(Convert.ToDouble(txtFeet.Text) >= 5))
            {
                MessageBox.Show("身高必去大于等于5");
                txtFeet.Select();
                return;
            }

            /*
             * v1.0计算逻辑
             */
            if (rbtnMale.Checked)
            {
                txtCalories.Text = (66
                                    + (6.3 * Convert.ToDouble(txtWeight.Text))
                                    + (12.9 * ((Convert.ToDouble(txtFeet.Text)*12)
                                           +Convert.ToDouble(txtInches.Text)) )
                                    -(6.8 * Convert.ToDouble(txtAge.Text))).ToString();
                    //v1.2增加计算合理体重
                    txtIdealWeight.Text = ((50
                                            + (2.3 * (((Convert.ToDouble(txtFeet.Text) - 5) * 12)
                                                      + Convert.ToDouble(txtInches.Text))))
                                           * 2.2046).ToString();
            }
            else
            {
                txtCalories.Text = (655
                                    + (4.3 * Convert.ToDouble(txtWeight.Text))
                                    + (4.7 * ((Convert.ToDouble(txtFeet.Text) * 12)
                                               + Convert.ToDouble(txtInches.Text)))
                                    - (4.7 * Convert.ToDouble(txtAge.Text))).ToString();
                //v1.2增加计算合理体重
                txtIdealWeight.Text = ((45.5
                                        + (2.3 * (((Convert.ToDouble(txtFeet.Text) - 5) * 12)
                                                  + Convert.ToDouble(txtInches.Text))))
                                       * 2.2046).ToString();
            }
            //v1.2 计算与理想体重的差值
            txtDistance.Text = (Convert.ToDouble(txtWeight.Text) - Convert.ToDouble(txtIdealWeight.Text)).ToString();
        }
    }
}
