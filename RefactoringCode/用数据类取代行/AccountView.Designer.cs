namespace DataBaseCmd2Class
{
    partial class AccountView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.viewAccountDetails = new System.Windows.Forms.Button();
            this.Number = new System.Windows.Forms.TextBox();
            this.Name = new System.Windows.Forms.TextBox();
            this.Type = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Balance = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // viewAccountDetails
            // 
            this.viewAccountDetails.Location = new System.Drawing.Point(126, 222);
            this.viewAccountDetails.Name = "viewAccountDetails";
            this.viewAccountDetails.Size = new System.Drawing.Size(75, 23);
            this.viewAccountDetails.TabIndex = 0;
            this.viewAccountDetails.Text = "viewAccountDetails";
            this.viewAccountDetails.UseVisualStyleBackColor = true;
            this.viewAccountDetails.Click += new System.EventHandler(this.viewAccountDetails_Click);
            // 
            // Number
            // 
            this.Number.Location = new System.Drawing.Point(114, 60);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(100, 21);
            this.Number.TabIndex = 1;
            // 
            // Name
            // 
            this.Name.Location = new System.Drawing.Point(114, 100);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(100, 21);
            this.Name.TabIndex = 2;
            // 
            // Type
            // 
            this.Type.Location = new System.Drawing.Point(114, 142);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(100, 21);
            this.Type.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Balance";
            // 
            // Balance
            // 
            this.Balance.Location = new System.Drawing.Point(114, 181);
            this.Balance.Name = "Balance";
            this.Balance.Size = new System.Drawing.Size(100, 21);
            this.Balance.TabIndex = 8;
            // 
            // AccountView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Balance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Type);
            this.Controls.Add(this.Name);
            this.Controls.Add(this.Number);
            this.Controls.Add(this.viewAccountDetails);
            ////this.Name = "AccountView";
            this.Text = "AccountView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button viewAccountDetails;
        private System.Windows.Forms.TextBox Number;
        private System.Windows.Forms.TextBox Name;
        private System.Windows.Forms.TextBox Type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Balance;
    }
}