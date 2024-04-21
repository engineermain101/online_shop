namespace shop_online
{
    partial class DetaliiProdus
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
            this.tabControlProdus = new System.Windows.Forms.TabControl();
            this.tabPageSpacificatii = new System.Windows.Forms.TabPage();
            this.dataGridViewSpecificatii = new System.Windows.Forms.DataGridView();
            this.Specificatie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalii = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageRecenzii = new System.Windows.Forms.TabPage();
            this.tabControlProdus.SuspendLayout();
            this.tabPageSpacificatii.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpecificatii)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlProdus
            // 
            this.tabControlProdus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProdus.Controls.Add(this.tabPageSpacificatii);
            this.tabControlProdus.Controls.Add(this.tabPageRecenzii);
            this.tabControlProdus.Location = new System.Drawing.Point(0, 0);
            this.tabControlProdus.Name = "tabControlProdus";
            this.tabControlProdus.SelectedIndex = 0;
            this.tabControlProdus.Size = new System.Drawing.Size(806, 458);
            this.tabControlProdus.TabIndex = 0;
            // 
            // tabPageSpacificatii
            // 
            this.tabPageSpacificatii.AccessibleName = "";
            this.tabPageSpacificatii.AutoScroll = true;
            this.tabPageSpacificatii.CausesValidation = false;
            this.tabPageSpacificatii.Controls.Add(this.dataGridViewSpecificatii);
            this.tabPageSpacificatii.Location = new System.Drawing.Point(4, 25);
            this.tabPageSpacificatii.Name = "tabPageSpacificatii";
            this.tabPageSpacificatii.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSpacificatii.Size = new System.Drawing.Size(798, 429);
            this.tabPageSpacificatii.TabIndex = 0;
            this.tabPageSpacificatii.Text = "Specificatii";
            this.tabPageSpacificatii.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSpecificatii
            // 
            this.dataGridViewSpecificatii.AllowUserToAddRows = false;
            this.dataGridViewSpecificatii.AllowUserToDeleteRows = false;
            this.dataGridViewSpecificatii.AllowUserToOrderColumns = true;
            this.dataGridViewSpecificatii.AllowUserToResizeColumns = false;
            this.dataGridViewSpecificatii.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSpecificatii.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSpecificatii.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewSpecificatii.BackgroundColor = System.Drawing.Color.Teal;
            this.dataGridViewSpecificatii.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSpecificatii.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Specificatie,
            this.Detalii});
            this.dataGridViewSpecificatii.Location = new System.Drawing.Point(17, 6);
            this.dataGridViewSpecificatii.Name = "dataGridViewSpecificatii";
            this.dataGridViewSpecificatii.RowTemplate.Height = 24;
            this.dataGridViewSpecificatii.Size = new System.Drawing.Size(678, 407);
            this.dataGridViewSpecificatii.TabIndex = 0;
            // 
            // Specificatie
            // 
            this.Specificatie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Specificatie.HeaderText = "Specificatie";
            this.Specificatie.Name = "Specificatie";
            this.Specificatie.ReadOnly = true;
            this.Specificatie.Width = 109;
            // 
            // Detalii
            // 
            this.Detalii.HeaderText = "Detalii";
            this.Detalii.Name = "Detalii";
            this.Detalii.ReadOnly = true;
            // 
            // tabPageRecenzii
            // 
            this.tabPageRecenzii.AutoScroll = true;
            this.tabPageRecenzii.Location = new System.Drawing.Point(4, 25);
            this.tabPageRecenzii.Name = "tabPageRecenzii";
            this.tabPageRecenzii.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecenzii.Size = new System.Drawing.Size(798, 429);
            this.tabPageRecenzii.TabIndex = 1;
            this.tabPageRecenzii.Text = "Recenzii";
            this.tabPageRecenzii.UseVisualStyleBackColor = true;
            // 
            // DetaliiProdus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlProdus);
            this.Name = "DetaliiProdus";
            this.Text = "DetaliiProdus";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DetaliiProdus_FormClosed);
            this.Load += new System.EventHandler(this.DetaliiProdus_Load);
            this.tabControlProdus.ResumeLayout(false);
            this.tabPageSpacificatii.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpecificatii)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProdus;
        private System.Windows.Forms.TabPage tabPageSpacificatii;
        private System.Windows.Forms.TabPage tabPageRecenzii;
        private System.Windows.Forms.DataGridView dataGridViewSpecificatii;
        private System.Windows.Forms.DataGridViewTextBoxColumn Specificatie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalii;
    }
}