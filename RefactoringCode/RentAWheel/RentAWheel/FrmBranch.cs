using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentAWheel
{
    public partial class FrmBranch : Form
    {
        private DataTable dtBranch;
        private int currentRowIndex;

        public FrmBranch()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            BranchName.Text = "";
        }

        private void BranchMaintenance_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            DataSet dsBranch = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Create Sql String 
            string strSql = "Select * from Branch";
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
                dtBranch = dsBranch.Tables[0];
                if (dtBranch.Rows.Count > 0)
                {
                    currentRowIndex = 0;
                    DataRow drRow = dtBranch.Rows[currentRowIndex];
                    txtId.Text = drRow["BranchId"].ToString();
                    BranchName.Text = drRow["Name"].ToString();
                }
            }
            catch
            {
                MessageBox.Show("A problem occurred and the application cannot recover! " +
                "Please contact the technical support.");
            }
        }

        private void DisplayCurrentRow()
        {
            DataRow drRow = dtBranch.Rows[currentRowIndex];
            txtId.Text = drRow["BranchId"].ToString();
            BranchName.Text = drRow["Name"].ToString();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (dtBranch.Rows.Count > currentRowIndex + 1)
            {
                currentRowIndex++;
                DisplayCurrentRow();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentRowIndex - 1 >= 0 & dtBranch.Rows.Count > 0)
            {
                currentRowIndex--;
                DisplayCurrentRow();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (dtBranch.Rows.Count > 0)
            {
                currentRowIndex = 0;
                DisplayCurrentRow();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (dtBranch.Rows.Count > 0)
            {
                currentRowIndex = dtBranch.Rows.Count - 1;
                DisplayCurrentRow();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSql;
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            if (txtId.Text.Equals(""))
            {
                //Create Sql String with parameter @SelectedLP
                strSql = "Insert Into Branch (Name) " + "Values(@Name)";
                //add parameter name
                command.Parameters.AddWithValue("@Name", BranchName.Text);
            }
            else
            {
                //Create Sql String with parameter @SelectedLP
                strSql = "Update Branch  Set Name = @Name " + "Where BranchId = @Id";
                //add parameter name
                command.Parameters.AddWithValue("@Name", BranchName.Text);
                //add parameter Id
                command.Parameters.AddWithValue("@Id", Convert.ToInt16(txtId.Text));
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = strSql;
                command.ExecuteNonQuery();            
                connection.Close();
            }
            catch
            {
                MessageBox.Show("A problem occurred and the application cannot recover! Please contact the technical support.");
            }
            BranchMaintenance_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(local);Initial Catalog=RENTAWHEELS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            //add parameter name
            string strSql = "Delete Branch " +  "Where BranchId = @Id";
            command.Parameters.AddWithValue("@Id", Convert.ToInt16(txtId.Text));
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = strSql;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("A problem occurred and the application cannot recover! " + "Please contact the technical support.");
            }
            BranchMaintenance_Load(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            BranchMaintenance_Load(null, null);
        }
    }
}

