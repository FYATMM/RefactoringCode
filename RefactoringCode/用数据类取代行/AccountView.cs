using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 两层结构，表示层直接和数据库通讯
/// 数据访问 和 其它API 常常在通信层中使用一个**行**的范式来检索各种数据
/// 比如System.Data.DataRow类
///
/// 用数据类替代：
/// 每个属性 对应 行中的项
/// </summary>
namespace DataBaseCmd2Class
{
    public partial class AccountView : Form
    {
        private DataTable accountTable;
        private string connectionString = "Data Source=(local);" + "Initial Catalog=RENTAWHEELS;" + "Integrated Security=True";

        public AccountView()
        {
            InitializeComponent();
        }
            public void viewAccountDetails_Click(object sender, System.EventArgs e)
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbDataAdapter adapter = new SqlDataAdapter();

                DataSet accountDataSet = new DataSet();

                IDbCommand command = new SqlCommand();
                string sql = "Select * from AccountsDemo where Number = " + Number.Text;

                connection.Open();
                command.Connection = connection;
                command.CommandText = sql;
                adapter.SelectCommand = command;
                adapter.Fill(accountDataSet);
                connection.Close();

                accountTable = accountDataSet.Tables[0];
                DataRow accountRow = accountTable.Rows[0];
                //Fill controls on the form
                Name.Text = accountRow["Name"].ToString();
                Type.Text = accountRow["Type"].ToString();
                if(!Convert.ToBoolean(accountRow["Blocked"]))
                {
                    Balance.Text = accountRow["Balance"].ToString();
                }
                else
                {
                    Balance.Text = "Blocked";
                }
            }

            }

    }
    

