using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RentAWheel
{
    public class BranchData
    {
        private DataTable branches;

        private string connectionString = "Data Source=(local);" + "Initial Catalog=RENTAWHEELS;" + "Integrated Security=True";


        private const String idParameterName = "@Id";
        private const String nameParameterName = "@Name";
        private const string branchTableIdColumnName = "BranchId";
        private const string branchTableNameColumnName = "Name";
        //用常量替换SQL字符串字面值
        private const string selectAllFromBranchSql = "Select * from Branch";
        const String insertBranchSql = "Insert Into Branch (Name)  Values(@Name)";
        const String updateBranchSql = "Update Branch  Set Name = @Name Where BranchId = @Id";
        private const String deleteBranchSql = "Delete Branch Where BranchId = @Id";

        private const int singleTableInDatasetIndex = 0;
        

        public IList<Branch> GetAll()
        {
            IList<Branch> branches = new List<Branch>();
            IDbCommand command = new SqlCommand();
            DataSet branchesSet = FillDataset(command, selectAllFromBranchSql);
            DataTable branchesTable = branchesSet.Tables[singleTableInDatasetIndex];
            foreach (DataRow row in branchesTable.Rows)
            {
                branches.Add(BranchFromRow(row));
            }
            return branches;
        }

        public static Branch BranchFromRow(DataRow row)
        {
            return new Branch(Convert.ToInt32(row[branchTableIdColumnName]),Convert.ToString(row[branchTableNameColumnName]));
        }


        public void Delete(Branch branch)
        {
            IDbCommand command = new SqlCommand();////SqlCommand command = new SqlCommand();
            AddParameter(command, "@Id", DbType.Int16, Convert.ToInt16(branch.Id));
            ExecuteNonQueray(command, deleteBranchSql);
        }

        public void Insert(Branch branch)
        {
            IDbCommand command = new SqlCommand();
            AddParameter(command, nameParameterName, DbType.String, branch.Name); //// name.Text);//command.Parameters.AddWithValue("@Name", BranchName.Text);
            ExecuteNonQueray(command, insertBranchSql);
        }

        public void Update(Branch branch)
        {
            IDbCommand command = new SqlCommand();
            AddParameter(command, nameParameterName, DbType.String, branch.Name);////name.Text);
            AddParameter(command, idParameterName, DbType.Int16, branch.Id);////Convert.ToInt16(id.Text));
            ExecuteNonQueray(command, updateBranchSql);
        }



        #region sql命令字符串利用参数方法提取
        /*比针对sql的代码多了两个参数，但是改进了代码的数据库中立性，付出的代价不大
               //Create Sql String with parameter @SelectedLP
                strSql = "Insert Into Branch (Name) " + "Values(@Name)";
                //add parameter name
                command.Parameters.AddWithValue("@Name", BranchName.Text);
         */
        public void AddParameter(IDbCommand command, string parameterName, DbType parameterTpye, object parameterValue)
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
        public IDbConnection PrepareDataObject(IDbCommand command, string sql)
        {
            IDbConnection connection = new SqlConnection(connectionString);////SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            command.Connection = connection;
            command.CommandText = sql;
            return connection;
        }
        //执行命令
        public void ExecuteNonQueray(IDbCommand command, string sql)
        {
            IDbConnection connection = PrepareDataObject(command, sql);
            command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion
        
        public DataSet FillDataset(IDbCommand command, string strSql)
        {
            IDbConnection connection = PrepareDataObject(command,strSql);
            IDbDataAdapter adapter = new SqlDataAdapter(); //SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet Branches = new DataSet();
            adapter.SelectCommand = (SqlCommand)command;                //execute command
            adapter.Fill(Branches);                //fill DataSet
            connection.Close();
            return Branches;
        }
    }
}

