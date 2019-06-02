using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace RentAWheel
{
    public partial class BranchMaintenance : Form
    {
        ////public DataTable Branches { get; private set; }
        private int currentRowIndex;
        public BranchData data { get; set; }
        public IList<Branch> branches { get; set; }

        public BranchMaintenance()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            id.Text = string.Empty;//txtId.Text = "";
            name.Text = string.Empty; //"";
        }

        private void LoadBranches()
        {

            //DataSet branches = FillDataset(command, strSql).Tables[0];////dtBranch = FillDataset(command, strSql).Tables[0];
            ////Branches.SelectAllFromBranchSql;
            //Branches = branches.Tables[singleTableInDatasetIndex];
            branches = data.GetAll();
            
        }

        private void BranchMaintenance_Load(object sender, EventArgs e)
        {
            LoadBranches();
            if (branches.Count > 0)//(dtBranch.Rows.Count > 0)
            {
                currentRowIndex = 0;
                DisplayCurrentRow();
            }
        }
        #region 导航按键，显示不同行数据库内容
        //显示一行数据库内容方法提取
        private void DisplayCurrentRow()
        {
            ////Branch branch = Branches[currentRowIndex]; //DataRow drRow = branches.Rows[currentRowIndex];
            //id.Text = Branch.Id.ToString(); //id.Text = drRow[branchTableIdColumnName].ToString();
            //name.Text = Branch.Name; //name.Text = drRow[branchTableNameColumnName].ToString();
            Branch branch = branches[currentRowIndex];
            id.Text = branch.Id.ToString();
            name.Text = branch.Name;

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (branches.Count > currentRowIndex + 1) ////if (Branches.Rows.Count > currentRowIndex + 1)
            {
                currentRowIndex++;
                DisplayCurrentRow();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentRowIndex - 1 >= 0 & branches.Count > 0)
            {
                currentRowIndex--;
                DisplayCurrentRow();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (branches.Count > 0)
            {
                currentRowIndex = 0;
                DisplayCurrentRow();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (branches.Count > 0)
            {
                currentRowIndex = branches.Count - 1;
                DisplayCurrentRow();
            }
        }        
        #endregion
     
        private void SaveBranch()
        {
            IDbCommand command = new SqlCommand();////SqlCommand command = new SqlCommand();
            ////if (id.Text.Equals(""))
            if(string.IsNullOrEmpty(id.Text))
            {
                ////strSql = insertBranchSql;//Create Sql String with parameter @SelectedLP                
                data.Insert(new Branch(0,name.Text));
            }
            else
            {
                ////strSql = updateBranchSql;
                Branch branch = branches[currentRowIndex];
                branch.Name = name.Text;
                data.Update(branches[currentRowIndex]);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBranch();
            BranchMaintenance_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            data.Delete(branches[currentRowIndex]);////DeleteBranch();
            BranchMaintenance_Load(null, null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            BranchMaintenance_Load(null, null);
        }
    }
}

