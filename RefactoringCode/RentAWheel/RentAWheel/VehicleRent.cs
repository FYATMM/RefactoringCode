using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentAWheel
{
    public partial class VehicleRent : Form
    {
        public VehicleRent()
        {
            InitializeComponent();
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

private void btnRent_Click(object sender, EventArgs e)
{
    if (MessageBox.Show("Are you sure?", "Confirm",
    MessageBoxButtons.OKCancel) == DialogResult.OK)
    {
                ////string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
                string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
          //      SqlConnection connection = new SqlConnection(
          //"Data Source=TESLATEAM;" +
          //"Initial Catalog=RENTAWHEELS;" +
          //"User ID=RENTAWHEELS_LOGIN;" +
          //"Password=RENTAWHEELS_PASSWORD_123");
        SqlCommand command;
        string strSql = "Update Vehicle " +
                    "Set Available = 1," +
                    "CustomerFirstName = @CustomerFirstName," +
                    "CustomerLastName = @CustomerLastName," +
                    "CustomerDocNumber = @CustomerDocNumber," +
                    "CustomerDocType = @CustomerDocType " +
                    "WHERE LicensePlate = @SelectedLP";
        command = new SqlCommand();
        try
        {//open connection
            connection.Open();
            //Set connection to command
            command.Connection = connection;
            //set Sql string to command object
            command.CommandText = strSql;
            //Add parameter to command
            command.Parameters.AddWithValue(
                "@CustomerFirstName", txtFirstName.Text);
            command.Parameters.AddWithValue(
                "@CustomerLastName", txtLastName.Text);
            command.Parameters.AddWithValue(
                "@CustomerDocNumber", txtDocumentNo.Text);
            command.Parameters.AddWithValue(
                "@CustomerDocType", txtDocumentType.Text);
            command.Parameters.AddWithValue(
                "@SelectedLP", txtLP.Text);
            //exexute command
            command.ExecuteNonQuery();
            //close connection
            connection.Close();
        }
        catch
        {
            MessageBox.Show("A problem occurred" + 
            "and the application cannot recover! " +
            "Please contact the technical support.");
        }
        this.Close();
    }

}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRt_Load(object sender, EventArgs e)
        {

        }
    }
}
