using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace DataBaseCmd2Class
{
    public class Account
    {
        private decimal balance;
        public string Number { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public decimal Balance
        {
            get => Blocked ? 0 : balance;
            set => balance = value;
        }
        public bool Blocked { get; set; }

        public string connectionString = "Data Source=(local);" + "Initial Catalog=RENTAWHEELS;" + "Integrated Security=True";

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
            ////Account account = new Account();
            Number = accountRow["Number"].ToString();
            Name = accountRow["Name"].ToString();
            Type = accountRow["Type"].ToString();
            Balance = Convert.ToDecimal(accountRow["Balance"]);
            Blocked = Convert.ToBoolean(accountRow["Blocked"]);
        }
    }
}
