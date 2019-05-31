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
///
/// 数据层与表示层分离
/// 将数据层提取到业务层：
///
/// 数据层与业务层分离
/// 将数据层单独提取为类：
/// 将业务层构造为含参数初始化：
/// 在数据层中方法初始化业务层数据：
/// </summary>
namespace DataBaseCmd2Class
{
    public partial class AccountView : Form
    {
        public AccountView()
        {
            InitializeComponent();
        }
        public void viewAccountDetails_Click(object sender, System.EventArgs e)
        {
            AccountData data = new AccountData();
            Account account = data.GetAccount(Number.Text);
            ////account.AccountData.GetAccount(Number.Text);
            //Fill controls on the form
            Name.Text = account.Name; //Name.Text = accountRow["Name"].ToString();
            Type.Text = account.Type; //Type.Text = accountRow["Type"].ToString();
            Balance.Text = account.Balance.ToString(CultureInfo.InvariantCulture); ////Balance.Text = accountRow["Balance"].ToString();
        }
    }

}


