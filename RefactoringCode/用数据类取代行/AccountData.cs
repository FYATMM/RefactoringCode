using System;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseCmd2Class
{
    public class AccountData
    {
        private Account _account;
        public string connectionString = "Data Source=(local);" + "Initial Catalog=RENTAWHEELS;" + "Integrated Security=True";

        public AccountData(Account account)
        {
            _account = account;
        }

        public void GetAccount(string number)
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
            _account.Number = accountRow["Number"].ToString();
            _account.Name = accountRow["Name"].ToString();
            _account.Type = accountRow["Type"].ToString();
            _account.Balance = Convert.ToDecimal(accountRow["Balance"]);
            _account.Blocked = Convert.ToBoolean(accountRow["Blocked"]);
        }
    }
}