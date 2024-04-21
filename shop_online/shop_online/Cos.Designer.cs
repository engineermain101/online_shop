namespace shop_online
{
    partial class Cos
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
            this.buttonCumpara = new System.Windows.Forms.Button();
            this.flowLayoutPanelProduse = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonElimina = new System.Windows.Forms.Button();
            this.labelPretTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCumpara
            // 
            this.buttonCumpara.Location = new System.Drawing.Point(580, 365);
            this.buttonCumpara.Name = "buttonCumpara";
            this.buttonCumpara.Size = new System.Drawing.Size(154, 95);
            this.buttonCumpara.TabIndex = 0;
            this.buttonCumpara.Text = "Cumpara";
            this.buttonCumpara.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelProduse
            // 
            this.flowLayoutPanelProduse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanelProduse.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelProduse.Name = "flowLayoutPanelProduse";
            this.flowLayoutPanelProduse.Size = new System.Drawing.Size(422, 526);
            this.flowLayoutPanelProduse.TabIndex = 1;
            // 
            // buttonElimina
            // 
            this.buttonElimina.Location = new System.Drawing.Point(580, 255);
            this.buttonElimina.Name = "buttonElimina";
            this.buttonElimina.Size = new System.Drawing.Size(154, 95);
            this.buttonElimina.TabIndex = 2;
            this.buttonElimina.Text = "Elimina din cos.";
            this.buttonElimina.UseVisualStyleBackColor = true;
            // 
            // labelPretTotal
            // 
            this.labelPretTotal.AutoSize = true;
            this.labelPretTotal.Location = new System.Drawing.Point(510, 69);
            this.labelPretTotal.Name = "labelPretTotal";
            this.labelPretTotal.Size = new System.Drawing.Size(73, 17);
            this.labelPretTotal.TabIndex = 3;
            this.labelPretTotal.Text = "Pret total: ";
            // 
            // Cos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 518);
            this.Controls.Add(this.labelPretTotal);
            this.Controls.Add(this.buttonElimina);
            this.Controls.Add(this.flowLayoutPanelProduse);
            this.Controls.Add(this.buttonCumpara);
            this.Name = "Cos";
            this.Text = "Cos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCumpara;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProduse;
        private System.Windows.Forms.Button buttonElimina;
        private System.Windows.Forms.Label labelPretTotal;
    }
}