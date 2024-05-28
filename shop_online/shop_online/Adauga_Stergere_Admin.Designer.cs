namespace shop_online
{
    partial class Adauga_Stergere_Admin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
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
            this.panelAdauga_Sterge_Admin = new System.Windows.Forms.Panel();
            this.buttonAdauga_Sterge = new System.Windows.Forms.Button();
            this.labelRol = new System.Windows.Forms.Label();
            this.comboBoxRol = new System.Windows.Forms.ComboBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.comboBoxEmail = new System.Windows.Forms.ComboBox();
            this.panelAdauga_Sterge_Admin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAdauga_Sterge_Admin
            // 
            this.panelAdauga_Sterge_Admin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelAdauga_Sterge_Admin.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelAdauga_Sterge_Admin.Controls.Add(this.buttonAdauga_Sterge);
            this.panelAdauga_Sterge_Admin.Controls.Add(this.labelRol);
            this.panelAdauga_Sterge_Admin.Controls.Add(this.comboBoxRol);
            this.panelAdauga_Sterge_Admin.Controls.Add(this.labelEmail);
            this.panelAdauga_Sterge_Admin.Controls.Add(this.comboBoxEmail);
            this.panelAdauga_Sterge_Admin.Location = new System.Drawing.Point(144, 68);
            this.panelAdauga_Sterge_Admin.Name = "panelAdauga_Sterge_Admin";
            this.panelAdauga_Sterge_Admin.Size = new System.Drawing.Size(400, 300);
            this.panelAdauga_Sterge_Admin.TabIndex = 0;
            // 
            // buttonAdauga_Sterge
            // 
            this.buttonAdauga_Sterge.Location = new System.Drawing.Point(180, 175);
            this.buttonAdauga_Sterge.Name = "buttonAdauga_Sterge";
            this.buttonAdauga_Sterge.Size = new System.Drawing.Size(165, 91);
            this.buttonAdauga_Sterge.TabIndex = 4;
            this.buttonAdauga_Sterge.Text = "Adauga";
            this.buttonAdauga_Sterge.UseVisualStyleBackColor = true;
            this.buttonAdauga_Sterge.Click += new System.EventHandler(this.buttonAdauga_Sterge_Click);
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Location = new System.Drawing.Point(17, 91);
            this.labelRol.Name = "labelRol";
            this.labelRol.Size = new System.Drawing.Size(29, 17);
            this.labelRol.TabIndex = 3;
            this.labelRol.Text = "Rol";
            // 
            // comboBoxRol
            // 
            this.comboBoxRol.FormattingEnabled = true;
            this.comboBoxRol.Location = new System.Drawing.Point(147, 87);
            this.comboBoxRol.Name = "comboBoxRol";
            this.comboBoxRol.Size = new System.Drawing.Size(230, 24);
            this.comboBoxRol.TabIndex = 2;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(17, 20);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(42, 17);
            this.labelEmail.TabIndex = 1;
            this.labelEmail.Text = "Email";
            // 
            // comboBoxEmail
            // 
            this.comboBoxEmail.FormattingEnabled = true;
            this.comboBoxEmail.Location = new System.Drawing.Point(147, 16);
            this.comboBoxEmail.Name = "comboBoxEmail";
            this.comboBoxEmail.Size = new System.Drawing.Size(230, 24);
            this.comboBoxEmail.TabIndex = 0;
            // 
            // Adauga_Stergere_Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 441);
            this.Controls.Add(this.panelAdauga_Sterge_Admin);
            this.Name = "Adauga_Stergere_Admin";
            this.Text = "Adauga_Stergere_Admin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Adauga_Stergere_Admin_FormClosed);
            this.Load += new System.EventHandler(this.Adauga_Stergere_Admin_Load);
            this.panelAdauga_Sterge_Admin.ResumeLayout(false);
            this.panelAdauga_Sterge_Admin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAdauga_Sterge_Admin;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.ComboBox comboBoxRol;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.ComboBox comboBoxEmail;
        private System.Windows.Forms.Button buttonAdauga_Sterge;
    }
}