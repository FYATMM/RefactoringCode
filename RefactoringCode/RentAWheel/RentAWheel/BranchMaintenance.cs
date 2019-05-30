using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentAWheel
{
    public partial class BranchMaintenance : Form
    {
        private DataTable branches;
        private int currentRowIndex;
        private string connectionString = "Data Source=(local);" + "Initial Catalog=RENTAWHEELS;" + "Integrated Security=True";
        private const int singleTableInDatasetIndex = 0;

        private const string branchTableIdColumnName = "BranchId";
        private const string branchTableNameColumnName = "Name";

        private const String idParameterName = "@Id";
        private const String nameParameterName = "@Name";
        //用常量替换SQL字符串字面值
        private const string selectAllFromBranchSql = "Select * from Branch";
        const String insertBranchSql = "Insert Into Branch (Name)  Values(@Name)";
        const String updateBranchSql = "Update Branch  Set Name = @Name Where BranchId = @Id";
        private const String deleteBranchSql = "Delete Branch Where BranchId = @Id";
        public BranchMaintenance()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            id.Text = string.Empty;//txtId.Text = "";
            name.Text = string.Empty; //"";
        }
        //使用适配器填充数据
        private DataSet FillDataset(IDbCommand command, string strSql)
        {
            IDbConnection connection = new SqlConnection(connectionString);////SqlConnection connection = new SqlConnection(connectionString);
            DataSet Branches = new DataSet();
            IDbDataAdapter adapter = new SqlDataAdapter(); //SqlDataAdapter adapter = new SqlDataAdapter();
            connection.Open();
            command.Connection = connection;
            command.CommandText = strSql;
            adapter.SelectCommand = (SqlCommand)command;                //execute command
            adapter.Fill(Branches);                //fill DataSet
            connection.Close();
            return Branches;
        }

        private void LoadBranches()
        {
            IDbCommand command = new SqlCommand();////SqlCommand command = new SqlCommand();
            DataSet branches = FillDataset(command,selectAllFromBranchSql);//DataSet branches = FillDataset(command, strSql).Tables[0];////dtBranch = FillDataset(command, strSql).Tables[0];
            this.branches = branches.Tables[singleTableInDatasetIndex];
        }

        private void BranchMaintenance_Load(object sender, EventArgs e)
        {
            LoadBranches();
            if (branches.Rows.Count > 0)//(dtBranch.Rows.Count > 0)
            {
                currentRowIndex = 0;
                DisplayCurrentRow();
            }
        }
        #region 导航按键，显示不同行数据库内容
        //显示一行数据库内容方法提取
        private void DisplayCurrentRow()
        {
            DataRow drRow = branches.Rows[currentRowIndex];
            id.Text = drRow["BranchId"].ToString();
            name.Text = drRow["Name"].ToString();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (branches.Rows.Count > currentRowIndex + 1)
            {
                currentRowIndex++;
                DisplayCurrentRow();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentRowIndex - 1 >= 0 & branches.Rows.Count > 0)
            {
                currentRowIndex--;
                DisplayCurrentRow();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (branches.Rows.Count > 0)
            {
                currentRowIndex = 0;
                DisplayCurrentRow();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (branches.Rows.Count > 0)
            {
                currentRowIndex = branches.Rows.Count - 1;
                DisplayCurrentRow();
            }
        }
        #endregion

        #region sql命令字符串利用参数方法提取
        /*比针对sql的代码多了两个参数，但是改进了代码的数据库中立性，付出的代价不大
               //Create Sql String with parameter @SelectedLP
                strSql = "Insert Into Branch (Name) " + "Values(@Name)";
                //add parameter name
                command.Parameters.AddWithValue("@Name", BranchName.Text);
         */
        private void AddParameter(IDbCommand command, string parameterName, DbType parameterTpye, object parameterValue)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = parameterTpye;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }
        #endregion

        #region 创建连接，执行命令 方法提取
        //创建连接
        private IDbConnection PrepareDataObject(IDbCommand command, string sql)
        {
            IDbConnection connection = new SqlConnection(connectionString);////SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            command.Connection = connection;
            command.CommandText = sql;
            return connection;
        }
        //执行命令
        private void ExecuteNonQueray(IDbCommand command, string sql            )
        {
            IDbConnection connection = PrepareDataObject(command, sql);
            command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        private void SaveBranch()
        {
            IDbCommand command = new SqlCommand();////SqlCommand command = new SqlCommand();
            if (id.Text.Equals(""))
            {
                ////strSql = insertBranchSql;//Create Sql String with parameter @SelectedLP                
                AddParameter(command, nameParameterName, DbType.String, name.Text);//command.Parameters.AddWithValue("@Name", BranchName.Text);
                ExecuteNonQueray(command, insertBranchSql);
            }
            else
            {
                ////strSql = updateBranchSql;
                AddParameter(command, "@Name", DbType.String, name.Text);
                AddParameter(command, idParameterName, DbType.Int16, Convert.ToInt16(id.Text));
                ExecuteNonQueray(command, updateBranchSql);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBranch();
            BranchMaintenance_Load(null, null);
        }

        private void DeleteBranch()
        {
            IDbCommand command = new SqlCommand();////SqlCommand command = new SqlCommand();
            AddParameter(command, "@Id", DbType.Int16, Convert.ToInt16(id.Text));
            ExecuteNonQueray(command, deleteBranchSql);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBranch();
            BranchMaintenance_Load(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            BranchMaintenance_Load(null, null);
        }
    }
}

