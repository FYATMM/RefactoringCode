﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RentAWheel
{
    public partial class FrmRcv : Form
    {
        public FrmRcv()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            int tankLevel = 0;
            switch (cboTank.Text)
            {
                case "Empty":
                    tankLevel = 0;
                    break;
                case "1/4":
                    tankLevel = 1;
                    break;
                case "1/2":
                    tankLevel = 2;
                    break;
                case "3/4":
                    tankLevel = 3;
                    break;
                case "Full":
                    tankLevel = 4;
                    break;
            }

            ////string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            //SqlConnection connection = new SqlConnection(
            //    "Data Source=TESLATEAM;" +
            //    "Initial Catalog=RENTAWHEELS;" +
            //     "User ID=RENTAWHEELS_LOGIN;Password=RENTAWHEELS_PASSWORD_123");
            SqlCommand command = new SqlCommand();
            //add parameter name
            string strSql = "Update Vehicle " +
                    "Set Available = 3," +
                    "Mileage = @Mileage," +
                    "Tank = @Tank " +
                    "WHERE LicensePlate = @SelectedLP";
           //Add parameter to command
            command.Parameters.AddWithValue(
                "@Mileage", txtMileage.Text);
            command.Parameters.AddWithValue(
                "@Tank", tankLevel);
            command.Parameters.AddWithValue(
                 "@SelectedLP", txtLP.Text);
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
            this.Close();
        }

        private void FrmRcv_Load(object sender, EventArgs e)
        {

        }
    }
}
