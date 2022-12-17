
namespace Secured_chat
{
    partial class ChatWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.Dialogbox = new System.Windows.Forms.RichTextBox();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.receiverName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dialogue";
            // 
            // Dialogbox
            // 
            this.Dialogbox.Location = new System.Drawing.Point(12, 57);
            this.Dialogbox.Name = "Dialogbox";
            this.Dialogbox.ReadOnly = true;
            this.Dialogbox.Size = new System.Drawing.Size(327, 530);
            this.Dialogbox.TabIndex = 1;
            this.Dialogbox.Text = "";
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(410, 312);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(333, 135);
            this.messageBox.TabIndex = 2;
            this.messageBox.Text = "";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(479, 501);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(193, 63);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Envoyer";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // receiverName
            // 
            this.receiverName.AutoSize = true;
            this.receiverName.Location = new System.Drawing.Point(568, 86);
            this.receiverName.Name = "receiverName";
            this.receiverName.Size = new System.Drawing.Size(35, 13);
            this.receiverName.TabIndex = 4;
            this.receiverName.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(407, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Votre message";
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 634);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.receiverName);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.Dialogbox);
            this.Controls.Add(this.label1);
            this.Name = "ChatWindow";
            this.Text = "ChatWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.Button sendButton;
        public System.Windows.Forms.Label receiverName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RichTextBox Dialogbox;
    }
}