namespace shop_online
{
    partial class ProdusCos
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDownNrBucati = new System.Windows.Forms.NumericUpDown();
            this.productControlCos = new shop_online.ProductControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNrBucati)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownNrBucati
            // 
            this.numericUpDownNrBucati.Location = new System.Drawing.Point(523, 45);
            this.numericUpDownNrBucati.Name = "numericUpDownNrBucati";
            this.numericUpDownNrBucati.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownNrBucati.TabIndex = 1;
            // 
            // productControlCos
            // 
            this.productControlCos.BackColor = System.Drawing.Color.DarkSalmon;
            this.productControlCos.Location = new System.Drawing.Point(3, 3);
            this.productControlCos.Name = "productControlCos";
            this.productControlCos.Size = new System.Drawing.Size(505, 138);
            this.productControlCos.TabIndex = 0;
            // 
            // ProdusCos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.numericUpDownNrBucati);
            this.Controls.Add(this.productControlCos);
            this.Name = "ProdusCos";
            this.Size = new System.Drawing.Size(612, 183);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNrBucati)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ProductControl productControlCos;
        private System.Windows.Forms.NumericUpDown numericUpDownNrBucati;
    }
}
