using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentAWheel
{
    public partial class FrmFlt : Form
    {
        //Maintain BranchId - BranchName relation
        private Hashtable branchIdTable;
        //Maintain ModelId - ModelName relation
        private Hashtable modelIdTable;
        //table Vehicles
        private DataTable dtVehicles;
        //Index of displayed row
        private int currentRowIndex;

        public FrmFlt()
        {
            InitializeComponent();
        }

        private void FrmFlt_Load(object sender, EventArgs e)
        {
            ////string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            //SqlConnection connection = new SqlConnection(
            //   "Data Source=TESLATEAM;" +
            //   "Initial Catalog=RENTAWHEELS;" +
            //   "User ID=RENTAWHEELS_LOGIN;" + 
            //   "Password=RENTAWHEELS_PASSWORD_123");
            SqlCommand oCmdCombo = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            //LOAD BRANCH COMBO -->
            //Create Sql String             
            string strSqlCombo = "Select * from Branch";
            try
            {
                //open connection
                connection.Open();
                //Set connection to command
                oCmdCombo.Connection = connection;
                //set Sql string to command object
                oCmdCombo.CommandText = strSqlCombo;
                //execute command
                SqlDataReader oReader = oCmdCombo.ExecuteReader();
                branchIdTable = new Hashtable();
                while (oReader.Read())
                {
                    cboBranch.Items.Add(oReader[1]);
                    //Add Id object to table with name as key
                    branchIdTable.Add(oReader[1], oReader[0]);
                }
                //close reader
                oReader.Close();
                //END LOAD COMBO -->

                //LOAD MODEL COMBO -->
                //Create Sql String             
                strSqlCombo = "Select * from Model";
                //Set connection to command
                oCmdCombo.Connection = connection;
                //set Sql string to command object
                oCmdCombo.CommandText = strSqlCombo;
                //execute command
                oReader = oCmdCombo.ExecuteReader();
                modelIdTable = new Hashtable();
                while (oReader.Read())
                {
                    cboModel.Items.Add(oReader[1]);
                    //Add Id object to table with name as key
                    modelIdTable.Add(oReader[1], oReader[0]);
                }
                //close reader
                oReader.Close();
                //END LOAD COMBO -->

                //LOAD DATASET -->
                //create data set
                DataSet dsModel = new DataSet();
                SqlCommand command = new SqlCommand();
                //Create Sql String with parameter @SelectedLP
                string strSql = "Select Vehicle.LicensePlate AS LicensePlate, " +
                "Branch.Name as BranchName, " +
                "Model.Name as ModelName " +
                "from Vehicle " +
                "Inner Join Branch On " +
                "Vehicle.BranchId = Branch.BranchId " +
                "Inner Join Model On " +
                "Vehicle.ModelId = Model.ModelId";
                //Set connection to command
                command.Connection = connection;
                //set Sql string to command object
                command.CommandText = strSql;
                //execute command
                adapter.SelectCommand = command;
                //fill DataSet
                adapter.Fill(dsModel);
                //close connection
                connection.Close();
                //destroy objects        
                dtVehicles = dsModel.Tables[0];
                if (dtVehicles.Rows.Count > 0)
                {
                    DataRow drRow = dtVehicles.Rows[0];
                    txtLP.Text = drRow["LicensePlate"].ToString();
                    cboBranch.Text = drRow["BranchName"].ToString();
                    cboModel.Text = drRow["ModelName"].ToString();
                    currentRowIndex = 0;
                }
                //END LOAD DATASET -->
            }
            catch
            {
                MessageBox.Show("A problem occurred and the application cannot recover! " +
                "Please contact the technical support.");
            }
        }

private void btnRight_Click(object sender, EventArgs e)
{
    if (dtVehicles.Rows.Count > currentRowIndex + 1)
    {
        currentRowIndex++;
        DataRow drRow = dtVehicles.Rows[currentRowIndex];
        txtLP.Text = drRow["LicensePlate"].ToString();
        cboBranch.Text = drRow["BranchName"].ToString();
        cboModel.Text = drRow["ModelName"].ToString();
    }
}

private void btnLeft_Click(object sender, EventArgs e)
{
    if (currentRowIndex - 1 >= 0 & dtVehicles.Rows.Count > 0)
    {
        currentRowIndex--;
        DataRow drRow = dtVehicles.Rows[currentRowIndex];
        txtLP.Text = drRow["LicensePlate"].ToString();
        cboBranch.Text = drRow["BranchName"].ToString();
        cboModel.Text = drRow["ModelName"].ToString();
    }
}

private void btnFirst_Click(object sender, EventArgs e)
{
    if (dtVehicles.Rows.Count > 0)
    {
        currentRowIndex = 0;
        DataRow drRow = dtVehicles.Rows[currentRowIndex];
        txtLP.Text = drRow["LicensePlate"].ToString();
        cboBranch.Text = drRow["BranchName"].ToString();
        cboModel.Text = drRow["ModelName"].ToString();
    }
}

private void btnLast_Click(object sender, EventArgs e)
{
    if (dtVehicles.Rows.Count > 0)
    {
        currentRowIndex = dtVehicles.Rows.Count - 1;
        DataRow drRow = dtVehicles.Rows[currentRowIndex];
        txtLP.Text = drRow["LicensePlate"].ToString();
        cboBranch.Text = drRow["BranchName"].ToString();
        cboModel.Text = drRow["ModelName"].ToString();
    }
}

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtLP.Text = "";
            cboBranch.Text = "";
            cboModel.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // 声明了变量“strSql”，但从未使用过
            string strSql;
#pragma warning restore CS0168 // 声明了变量“strSql”，但从未使用过
            ////string connectionString = ConfigurationManager.ConnectionStrings["SQLCONNECTIONSTRING"].ConnectionString;
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            //SqlConnection connection = new SqlConnection(
            //   "Data Source=TESLATEAM;" +
            //   "Initial Catalog=RENTAWHEELS;" +
            //   "User ID=RENTAWHEELS_LOGIN;" + 
            //   "Password=RENTAWHEELS_PASSWORD_123");
            SqlCommand oCmdDelete = new SqlCommand();
            SqlCommand oCmdInsert = new SqlCommand();
            string strSqlDelete = "Delete From Vehicle " + 
                 "Where LicensePlate = @LicensePlate";
            //Create Sql String for insert
            string strSqlInsert = "Insert Into Vehicle " + 
            "(LicensePlate, ModelId,BranchId) " + 
            "Values(@LicensePlate, @ModelId, @BranchId)";
            //add parameter for delete
            oCmdDelete.Parameters.AddWithValue( 
            "@LicensePlate", txtLP.Text);
            //add parameters for insert
            oCmdInsert.Parameters.AddWithValue( 
                "@LicensePlate", txtLP.Text);
            oCmdInsert.Parameters.AddWithValue( 
            "@ModelId", modelIdTable[cboModel.Text]);
            oCmdInsert.Parameters.AddWithValue( 
                "@BranchId", branchIdTable[cboBranch.Text]);
            //open connection
            connection.Open();
            //Set connection to command
            oCmdDelete.Connection = connection;
            oCmdInsert.Connection = connection;
            //set Sql string to command object
            oCmdDelete.CommandText = strSqlDelete;
            oCmdInsert.CommandText = strSqlInsert;
            //start transaction
            SqlTransaction oTrx = connection.BeginTransaction();
            //enlist commands with transaction
            oCmdDelete.Transaction = oTrx;
            oCmdInsert.Transaction = oTrx;
            //execute command: first delete and then insert record
            oCmdDelete.ExecuteNonQuery();
            oCmdInsert.ExecuteNonQuery();
            oTrx.Commit();
            //close connection
            connection.Close();                                              
            FrmFlt_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {            
            SqlConnection connection = new SqlConnection(
               "Data Source=TESLATEAM;" +
               "Initial Catalog=RENTAWHEELS;" +
               "User ID=RENTAWHEELS_LOGIN;" + 
               "Password=RENTAWHEELS_PASSWORD_123");
            SqlCommand oCmdDelete = new SqlCommand();            
            string strSqlDelete = "Delete From Vehicle " +
                 "Where LicensePlate = @LicensePlate";           
            //add parameter for delete
            oCmdDelete.Parameters.AddWithValue(
            "@LicensePlate", txtLP.Text);            
            //open connection
            connection.Open();
            //Set connection to command
            oCmdDelete.Connection = connection;            
            //set Sql string to command object
            oCmdDelete.CommandText = strSqlDelete;                   
            //execute command: delete 
            oCmdDelete.ExecuteNonQuery();
            //close connection
            connection.Close();
            FrmFlt_Load(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            FrmFlt_Load(null, null);
        }

    }
}

