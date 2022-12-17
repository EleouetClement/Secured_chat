
namespace Secured_chat
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ipAddress = new System.Windows.Forms.TextBox();
            this.Connexion = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.portNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.attempts = new System.Windows.Forms.Label();
            this.pseudo = new System.Windows.Forms.Label();
            this.pseudovalue = new System.Windows.Forms.TextBox();
            this.listeningPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ip du serveur (IPv4) : ";
            // 
            // ipAddress
            // 
            this.ipAddress.Location = new System.Drawing.Point(156, 18);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(163, 20);
            this.ipAddress.TabIndex = 1;
            // 
            // Connexion
            // 
            this.Connexion.Location = new System.Drawing.Point(143, 141);
            this.Connexion.Name = "Connexion";
            this.Connexion.Size = new System.Drawing.Size(121, 21);
            this.Connexion.TabIndex = 5;
            this.Connexion.Text = "button1";
            this.Connexion.UseVisualStyleBackColor = true;
            this.Connexion.Click += new System.EventHandler(this.Connexion_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port du serveur : ";
            // 
            // portNumber
            // 
            this.portNumber.Location = new System.Drawing.Point(156, 48);
            this.portNumber.Name = "portNumber";
            this.portNumber.Size = new System.Drawing.Size(78, 20);
            this.portNumber.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tentatives de connexions : ";
            // 
            // attempts
            // 
            this.attempts.AutoSize = true;
            this.attempts.Location = new System.Drawing.Point(156, 182);
            this.attempts.Name = "attempts";
            this.attempts.Size = new System.Drawing.Size(42, 13);
            this.attempts.TabIndex = 9;
            this.attempts.Text = "attempt";
            // 
            // pseudo
            // 
            this.pseudo.AutoSize = true;
            this.pseudo.Location = new System.Drawing.Point(73, 82);
            this.pseudo.Name = "pseudo";
            this.pseudo.Size = new System.Drawing.Size(77, 13);
            this.pseudo.TabIndex = 10;
            this.pseudo.Text = "Pseudonyme : ";
            // 
            // pseudovalue
            // 
            this.pseudovalue.Location = new System.Drawing.Point(156, 79);
            this.pseudovalue.Name = "pseudovalue";
            this.pseudovalue.Size = new System.Drawing.Size(100, 20);
            this.pseudovalue.TabIndex = 11;
            // 
            // listeningPort
            // 
            this.listeningPort.Location = new System.Drawing.Point(156, 105);
            this.listeningPort.Name = "listeningPort";
            this.listeningPort.Size = new System.Drawing.Size(100, 20);
            this.listeningPort.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Port d\'ecoute des messages : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 336);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listeningPort);
            this.Controls.Add(this.pseudovalue);
            this.Controls.Add(this.pseudo);
            this.Controls.Add(this.attempts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Connexion);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipAddress;
        private System.Windows.Forms.Button Connexion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label attempts;
        private System.Windows.Forms.Label pseudo;
        private System.Windows.Forms.TextBox pseudovalue;
        private System.Windows.Forms.TextBox listeningPort;
        private System.Windows.Forms.Label label4;
    }
}

