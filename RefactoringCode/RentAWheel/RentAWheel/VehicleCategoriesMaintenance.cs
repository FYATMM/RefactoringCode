using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentAWheel
{
    public partial class VehicleCategoriesMaintenance : Form
    {
        private DataTable dtCategory;
        private int currentRowIndex;

        public VehicleCategoriesMaintenance()
        {
            InitializeComponent();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (dtCategory.Rows.Count > currentRowIndex + 1)
            {
                currentRowIndex++;
                DataRow drRow = dtCategory.Rows[currentRowIndex];
                txtId.Text = drRow["CategoryId"].ToString();
                BranchName.Text = drRow["Name"].ToString();
                txtDailyPrice.Text = drRow["DailyPrice"].ToString();
                txtWeeklyPrice.Text = drRow["WeeklyPrice"].ToString();
                txtMonthlyPrice.Text = drRow["MonthlyPrice"].ToString();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentRowIndex - 1 >= 0 & dtCategory.Rows.Count > 0)
            {
                currentRowIndex--;
                DataRow drRow = dtCategory.Rows[currentRowIndex];
                txtId.Text = drRow["CategoryId"].ToString();
                BranchName.Text = drRow["Name"].ToString();
                txtDailyPrice.Text = drRow["DailyPrice"].ToString();
                txtWeeklyPrice.Text = drRow["WeeklyPrice"].ToString();
                txtMonthlyPrice.Text = drRow["MonthlyPrice"].ToString();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (dtCategory.Rows.Count > 0)
            {
                currentRowIndex = 0;
                DataRow drRow = dtCategory.Rows[currentRowIndex];
                txtId.Text = drRow["CategoryId"].ToString();
                BranchName.Text = drRow["Name"].ToString();
                txtDailyPrice.Text = drRow["DailyPrice"].ToString();
                txtWeeklyPrice.Text = drRow["WeeklyPrice"].ToString();
                txtMonthlyPrice.Text = drRow["MonthlyPrice"].ToString();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (dtCategory.Rows.Count > 0)
            {
                currentRowIndex = dtCategory.Rows.Count - 1;
                DataRow drRow = dtCategory.Rows[currentRowIndex];
                txtId.Text = drRow["CategoryId"].ToString();
                BranchName.Text = drRow["Name"].ToString();
                txtDailyPrice.Text = drRow["DailyPrice"].ToString();
                txtWeeklyPrice.Text = drRow["WeeklyPrice"].ToString();
                txtMonthlyPrice.Text = drRow["MonthlyPrice"].ToString();
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            BranchName.Text = "";
            txtDailyPrice.Text = "";
            txtWeeklyPrice.Text = "";
            txtMonthlyPrice.Text = "";
        }

        private void FrmCat_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            //SqlConnection connection = new SqlConnection(
            //   "Data Source=TESLATEAM;" +
            //   "Initial Catalog=RENTAWHEELS;" +
            //   "User ID=RENTAWHEELS_LOGIN;Password=RENTAWHEELS_PASSWORD_123");
            SqlCommand command = new SqlCommand();
            DataSet dsBranch = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Create Sql String 
            string strSql = "Select * from Category";
            try
            {
                connection.Open();
                //Set connection to command
                command.Connection = connection;
                //set Sql string to command object
                command.CommandText = strSql;
                //execute command
                adapter.SelectCommand = command;
                //fill DataSet
                adapter.Fill(dsBranch);
                //close connection
                connection.Close();
                dtCategory = dsBranch.Tables[0];
                if (dtCategory.Rows.Count > 0)
                {
                    currentRowIndex = 0;
                    DataRow drRow = dtCategory.Rows[currentRowIndex];
                    txtId.Text = drRow["CategoryId"].ToString();
                    BranchName.Text = drRow["Name"].ToString();
                    txtDailyPrice.Text = drRow["DailyPrice"].ToString();
                    txtWeeklyPrice.Text = drRow["WeeklyPrice"].ToString();
                    txtMonthlyPrice.Text = drRow["MonthlyPrice"].ToString();

                }
            }
            catch
            {
                MessageBox.Show("A problem occurred and the application cannot recover! " +
                "Please contact the technical support.");
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            FrmCat_Load(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSql;
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            //SqlConnection connection = new SqlConnection(
            //   "Data Source=TESLATEAM;" +
            //   "Initial Catalog=RENTAWHEELS;" +
            //   "User ID=RENTAWHEELS_LOGIN;Password=RENTAWHEELS_PASSWORD_123");
            SqlCommand command = new SqlCommand();
            if (txtId.Text.Equals(""))
            {
                //Create Sql String with parameters
                strSql = "Insert Into Category " +
                "(Name, MonthlyPrice, DailyPrice, WeeklyPrice) " +
                "Values(@Name,@MonthlyPrice, " +
                "@DailyPrice, @WeeklyPrice)";
                //add parameters
                command.Parameters.AddWithValue("@Name", BranchName.Text);
                command.Parameters.AddWithValue(
                    "@DailyPrice", Convert.ToDecimal(txtDailyPrice.Text));
                command.Parameters.AddWithValue(
                "@WeeklyPrice", Convert.ToDecimal(txtWeeklyPrice.Text));
                command.Parameters.AddWithValue(
                "@MonthlyPrice", Convert.ToDecimal(txtMonthlyPrice.Text));
            }
            else
            {
                //Create Sql String with parameter @SelectedLP
                strSql = "Update Category  Set Name = @Name, " +
                "DailyPrice = @DailyPrice, " +
                "WeeklyPrice = @WeeklyPrice, " +
                "MonthlyPrice = @MonthlyPrice " +
                "Where CategoryId = @CategoryId";
                //add parameters
                command.Parameters.AddWithValue("@Name", BranchName.Text);
                command.Parameters.AddWithValue(
                    "@DailyPrice", Convert.ToDecimal(txtDailyPrice.Text));
                command.Parameters.AddWithValue(
                    "@WeeklyPrice", Convert.ToDecimal(txtWeeklyPrice.Text));
                command.Parameters.AddWithValue(
                    "@MonthlyPrice", Convert.ToDecimal(txtMonthlyPrice.Text));
                command.Parameters.AddWithValue("@CategoryId",
                    Convert.ToInt16(txtId.Text));
            }
           try
            {
                //open connection
                connection.Open();
                //Set connection to command
                command.Connection = connection;
                //set Sql string to command object
                command.CommandText = strSql;
                //exexute command
                command.ExecuteNonQuery();
                //close connection
                connection.Close();
            }
           catch
            {
                MessageBox.Show("A problem occurred and the application cannot recover! " +
                "Please contact the technical support.");
            }
            FrmCat_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            //SqlConnection connection = new SqlConnection(
            // "Data Source=TESLATEAM;" +
            // "Initial Catalog=RENTAWHEELS;" +
            // "User ID=RENTAWHEELS_LOGIN;Password=RENTAWHEELS_PASSWORD_123");
            SqlCommand command = new SqlCommand();
            //add parameter name
            string strSql = "Delete Category " +
            "Where CategoryId = @Id";
            command.Parameters.AddWithValue("@Id", Convert.ToInt16(txtId.Text));
            try
            {
                //open connection
                connection.Open();
                //Set connection to command
                command.Connection = connection;
                //set Sql string to command object
                command.CommandText = strSql;
                //exexute command
                command.ExecuteNonQuery();
                //close connection
                connection.Close();
            }
            catch
            {
                MessageBox.Show("A problem occurred and the application cannot recover! " +
                "Please contact the technical support.");
            }
            FrmCat_Load(null, null);
        }




    }
}
