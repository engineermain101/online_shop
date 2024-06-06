namespace shop_online
{
    partial class Afisare_Produse
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanelProduse = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStripAdaugaProduse = new System.Windows.Forms.MenuStrip();
            this.produsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categorieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaProdusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaFurnizorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaugaAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stergereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stergereProdusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stergereFurnizorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stergereAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delogheazateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threaduriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonContextMenu1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStripAdaugaProduse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProduse
            // 
            this.flowLayoutPanelProduse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelProduse.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowLayoutPanelProduse.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLayoutPanelProduse.Location = new System.Drawing.Point(0, 44);
            this.flowLayoutPanelProduse.Name = "flowLayoutPanelProduse";
            this.flowLayoutPanelProduse.Size = new System.Drawing.Size(1019, 498);
            this.flowLayoutPanelProduse.TabIndex = 1;
            this.flowLayoutPanelProduse.Click += new System.EventHandler(this.flowLayoutPanelProduse_Click);
            this.flowLayoutPanelProduse.DoubleClick += new System.EventHandler(this.flowLayoutPanelProduse_DoubleClick);
            // 
            // menuStripAdaugaProduse
            // 
            this.menuStripAdaugaProduse.BackColor = System.Drawing.Color.Firebrick;
            this.menuStripAdaugaProduse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStripAdaugaProduse.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripAdaugaProduse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produsToolStripMenuItem,
            this.adaugaToolStripMenuItem,
            this.stergereToolStripMenuItem,
            this.cosToolStripMenuItem,
            this.delogheazateToolStripMenuItem,
            this.threaduriToolStripMenuItem});
            this.menuStripAdaugaProduse.Location = new System.Drawing.Point(0, 0);
            this.menuStripAdaugaProduse.Name = "menuStripAdaugaProduse";
            this.menuStripAdaugaProduse.Size = new System.Drawing.Size(1019, 39);
            this.menuStripAdaugaProduse.TabIndex = 1;
            this.menuStripAdaugaProduse.Text = "menuStrip1";
            // 
            // produsToolStripMenuItem
            // 
            this.produsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.categorieToolStripMenuItem});
            this.produsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.produsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.produsToolStripMenuItem.Image = global::shop_online.Properties.Resources.hamburger_button_computer_icons_menu_clip_art_menu_d61701f9c6eb726b2534cb2ca9181312;
            this.produsToolStripMenuItem.Name = "produsToolStripMenuItem";
            this.produsToolStripMenuItem.Size = new System.Drawing.Size(119, 35);
            this.produsToolStripMenuItem.Text = "Produs";
            // 
            // categorieToolStripMenuItem
            // 
            this.categorieToolStripMenuItem.Name = "categorieToolStripMenuItem";
            this.categorieToolStripMenuItem.Size = new System.Drawing.Size(201, 36);
            this.categorieToolStripMenuItem.Text = "Categorie";
            // 
            // adaugaToolStripMenuItem
            // 
            this.adaugaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adaugaProdusToolStripMenuItem,
            this.adaugaFurnizorToolStripMenuItem,
            this.adaugaAdminToolStripMenuItem});
            this.adaugaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adaugaToolStripMenuItem.Name = "adaugaToolStripMenuItem";
            this.adaugaToolStripMenuItem.Size = new System.Drawing.Size(108, 35);
            this.adaugaToolStripMenuItem.Text = "Adauga";
            // 
            // adaugaProdusToolStripMenuItem
            // 
            this.adaugaProdusToolStripMenuItem.Name = "adaugaProdusToolStripMenuItem";
            this.adaugaProdusToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.adaugaProdusToolStripMenuItem.Text = "Produs";
            this.adaugaProdusToolStripMenuItem.Click += new System.EventHandler(this.adaugaProdusToolStripMenuItem_Click);
            // 
            // adaugaFurnizorToolStripMenuItem
            // 
            this.adaugaFurnizorToolStripMenuItem.Name = "adaugaFurnizorToolStripMenuItem";
            this.adaugaFurnizorToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.adaugaFurnizorToolStripMenuItem.Text = "Furnizor";
            this.adaugaFurnizorToolStripMenuItem.Click += new System.EventHandler(this.adaugaFurnizorToolStripMenuItem_Click);
            // 
            // adaugaAdminToolStripMenuItem
            // 
            this.adaugaAdminToolStripMenuItem.Name = "adaugaAdminToolStripMenuItem";
            this.adaugaAdminToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.adaugaAdminToolStripMenuItem.Text = "Admin";
            this.adaugaAdminToolStripMenuItem.Click += new System.EventHandler(this.adaugaAdminToolStripMenuItem_Click);
            // 
            // stergereToolStripMenuItem
            // 
            this.stergereToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stergereProdusToolStripMenuItem,
            this.stergereFurnizorToolStripMenuItem,
            this.stergereAdminToolStripMenuItem});
            this.stergereToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stergereToolStripMenuItem.Name = "stergereToolStripMenuItem";
            this.stergereToolStripMenuItem.Size = new System.Drawing.Size(113, 35);
            this.stergereToolStripMenuItem.Text = "Stergere";
            // 
            // stergereProdusToolStripMenuItem
            // 
            this.stergereProdusToolStripMenuItem.Name = "stergereProdusToolStripMenuItem";
            this.stergereProdusToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.stergereProdusToolStripMenuItem.Text = "Produs";
            this.stergereProdusToolStripMenuItem.Visible = false;
            this.stergereProdusToolStripMenuItem.Click += new System.EventHandler(this.stergereProdusToolStripMenuItem_Click);
            // 
            // stergereFurnizorToolStripMenuItem
            // 
            this.stergereFurnizorToolStripMenuItem.Name = "stergereFurnizorToolStripMenuItem";
            this.stergereFurnizorToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.stergereFurnizorToolStripMenuItem.Text = "Furnizor";
            this.stergereFurnizorToolStripMenuItem.Click += new System.EventHandler(this.stergereFurnizorToolStripMenuItem_Click);
            // 
            // stergereAdminToolStripMenuItem
            // 
            this.stergereAdminToolStripMenuItem.Name = "stergereAdminToolStripMenuItem";
            this.stergereAdminToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.stergereAdminToolStripMenuItem.Text = "Admin";
            this.stergereAdminToolStripMenuItem.Click += new System.EventHandler(this.stergereAdminToolStripMenuItem_Click);
            // 
            // cosToolStripMenuItem
            // 
            this.cosToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cosToolStripMenuItem.Image = global::shop_online.Properties.Resources.pngegg;
            this.cosToolStripMenuItem.Name = "cosToolStripMenuItem";
            this.cosToolStripMenuItem.Size = new System.Drawing.Size(85, 35);
            this.cosToolStripMenuItem.Text = "Cos";
            this.cosToolStripMenuItem.Click += new System.EventHandler(this.cosToolStripMenuItem_Click);
            // 
            // delogheazateToolStripMenuItem
            // 
            this.delogheazateToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delogheazateToolStripMenuItem.Name = "delogheazateToolStripMenuItem";
            this.delogheazateToolStripMenuItem.Size = new System.Drawing.Size(177, 35);
            this.delogheazateToolStripMenuItem.Text = "Delogheaza-te";
            this.delogheazateToolStripMenuItem.Click += new System.EventHandler(this.delogheazateToolStripMenuItem_Click);
            // 
            // threaduriToolStripMenuItem
            // 
            this.threaduriToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threaduriToolStripMenuItem.Name = "threaduriToolStripMenuItem";
            this.threaduriToolStripMenuItem.Size = new System.Drawing.Size(126, 35);
            this.threaduriToolStripMenuItem.Text = "Threaduri";
            this.threaduriToolStripMenuItem.Visible = false;
            this.threaduriToolStripMenuItem.Click += new System.EventHandler(this.threaduriToolStripMenuItem_Click);
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.SystemColors.Control;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.SystemColors.Control;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 10;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.SystemColors.Info;
            // 
            // kryptonContextMenu1
            // 
            this.kryptonContextMenu1.Palette = this.kryptonPalette1;
            this.kryptonContextMenu1.StateCommon.ControlInner.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Firebrick;
            this.pictureBox1.Image = global::shop_online.Properties.Resources.user;
            this.pictureBox1.Location = new System.Drawing.Point(952, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Firebrick;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(938, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "numeUser";
            // 
            // Afisare_Produse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1019, 516);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanelProduse);
            this.Controls.Add(this.menuStripAdaugaProduse);
            this.Name = "Afisare_Produse";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.Text = "Afisare Produse";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Afisare_Produse_FormClosed);
            this.Load += new System.EventHandler(this.Afisare_Produse_Load);
            this.Resize += new System.EventHandler(this.Afisare_Produse_Resize);
            this.menuStripAdaugaProduse.ResumeLayout(false);
            this.menuStripAdaugaProduse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProduse;
        private System.Windows.Forms.MenuStrip menuStripAdaugaProduse;
        private System.Windows.Forms.ToolStripMenuItem produsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categorieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaugaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaugaProdusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaugaFurnizorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaugaAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stergereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stergereProdusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stergereFurnizorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stergereAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delogheazateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threaduriToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu kryptonContextMenu1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}