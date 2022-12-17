
namespace Secured_chat
{
    partial class UsersList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.allConnectedUsers = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.refreshList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // allConnectedUsers
            // 
            this.allConnectedUsers.FormattingEnabled = true;
            this.allConnectedUsers.Location = new System.Drawing.Point(67, 57);
            this.allConnectedUsers.Name = "allConnectedUsers";
            this.allConnectedUsers.Size = new System.Drawing.Size(268, 277);
            this.allConnectedUsers.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(459, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Discuter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // refreshList
            // 
            this.refreshList.Location = new System.Drawing.Point(459, 100);
            this.refreshList.Name = "refreshList";
            this.refreshList.Size = new System.Drawing.Size(191, 40);
            this.refreshList.TabIndex = 2;
            this.refreshList.Text = "Rafraichir liste";
            this.refreshList.UseVisualStyleBackColor = true;
            this.refreshList.Click += new System.EventHandler(this.refreshList_Click);
            // 
            // UsersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 406);
            this.Controls.Add(this.refreshList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.allConnectedUsers);
            this.Name = "UsersList";
            this.Text = "UsersList";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ListBox allConnectedUsers;
        private System.Windows.Forms.Button refreshList;
    }
}