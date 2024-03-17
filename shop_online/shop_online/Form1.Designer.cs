namespace shop_online
{
    partial class FormLogin
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
            this.panelLogin = new System.Windows.Forms.Panel();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.buttonSignUp);
            this.panelLogin.Controls.Add(this.buttonLogin);
            this.panelLogin.Location = new System.Drawing.Point(116, 45);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(558, 346);
            this.panelLogin.TabIndex = 0;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(92, 97);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(155, 116);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.Location = new System.Drawing.Point(312, 97);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(155, 116);
            this.buttonSignUp.TabIndex = 1;
            this.buttonSignUp.Text = "SignUp";
            this.buttonSignUp.UseVisualStyleBackColor = true;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelLogin);
            this.Name = "FormLogin";
            this.Text = "Login";
            this.panelLogin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Button buttonSignUp;
        private System.Windows.Forms.Button buttonLogin;
    }
}

