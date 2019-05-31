using System;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseCmd2Class
{
    public class AccountData
    {
        public string connectionString = "Data Source=(local);" + "Initial Catalog=RENTAWHEELS;" + "Integrated Security=True";

        public AccountData()
        {
            ////_account = account;
        }

        public Account GetAccount(string number)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            IDbDataAdapter adapter = new SqlDataAdapter();

            DataSet accountDataSet = new DataSet();

            IDbCommand command = new SqlCommand();
            string sql = "Select * from AccountsDemo where Number = " + number;

            connection.Open();
            command.Connection = connection;
            command.CommandText = sql;
            adapter.SelectCommand = command;
            adapter.Fill(accountDataSet);
            connection.Close();
            DataTable accountTable = accountDataSet.Tables[0];
            DataRow accountRow = accountTable.Rows[0];
            ////把数据库数据导入到account对象
            //_account.Number = accountRow["Number"].ToString();
            //_account.Name = accountRow["Name"].ToString();
            //_account.Type = accountRow["Type"].ToString();
            //_account.Balance = Convert.ToDecimal(accountRow["Balance"]);
            //_account.Blocked = Convert.ToBoolean(accountRow["Blocked"]);

            return  new Account(Convert.ToInt32(accountRow["Number"]), 
                accountRow["Name"].ToString(),
                accountRow["Type"].ToString(),
                Convert.ToDecimal(accountRow["Balance"]),
                Convert.ToBoolean(accountRow["Blocked"]));
        }
    }
}