using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

/// <summary>
/// 两层结构，表示层直接和数据库通讯
/// 数据访问 和 其它API 常常在通信层中使用一个**行**的范式来检索各种数据
/// 比如System.Data.DataRow类
///
/// 用数据类替代：V2.0
/// 每个属性 对应 行中的项
///
/// 将表示层与业务层分离：v3.0
/// 将业务逻辑移到之前定义的数据类中
/// 比如条件判断
/// </summary>
namespace DataBaseCmd2Class
{
    public partial class AccountView : Form
    {
        private DataTable accountTable;


        public AccountView()
        {
            InitializeComponent();
        }
        public void viewAccountDetails_Click(object sender, System.EventArgs e)
        {
            ////Account account = GetAccount(Number.Text);
            Account account = new Account();
            account.GetAccount(Number.Text);
            //Fill controls on the form
            Name.Text = account.Name; //Name.Text = accountRow["Name"].ToString();
            Type.Text = account.Type; //Type.Text = accountRow["Type"].ToString();
            Balance.Text = account.Balance.ToString(CultureInfo.InvariantCulture); ////Balance.Text = accountRow["Balance"].ToString();
        }

        //private Account GetAccount(string number)
        //{
        //    IDbConnection connection = new SqlConnection(connectionString);
        //    IDbDataAdapter adapter = new SqlDataAdapter();

        //    DataSet accountDataSet = new DataSet();

        //    IDbCommand command = new SqlCommand();
        //    string sql = "Select * from AccountsDemo where Number = " + Number.Text;

        //    connection.Open();
        //    command.Connection = connection;
        //    command.CommandText = sql;
        //    adapter.SelectCommand = command;
        //    adapter.Fill(accountDataSet);
        //    connection.Close();

        //    accountTable = accountDataSet.Tables[0];
        //    DataRow accountRow = accountTable.Rows[0];
        //    ////把数据库数据导入到account对象
        //    Account account = new Account();
        //    account.Number = accountRow["Number"].ToString();
        //    account.Name = accountRow["Name"].ToString();
        //    account.Type = accountRow["Type"].ToString();
        //    account.Balance = Convert.ToDecimal(accountRow["Balance"]);
        //    account.Blocked = Convert.ToBoolean(accountRow["Blocked"]);

        //    return account;
        //}
    }

}


