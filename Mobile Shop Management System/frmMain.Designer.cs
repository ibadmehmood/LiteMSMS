namespace Mobile_Shop_Management_System
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tblAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new Mobile_Shop_Management_System.DataSet1();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saleInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseInvoiceReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashBookToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saleInvoiceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saleInvoiceReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cashBookToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cashBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCostQuickSearchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCostQuickSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleProfitReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.purchaseReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userAccountReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addEditAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEditUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.tblAccountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblAccountBindingSource
            // 
            this.tblAccountBindingSource.DataMember = "tblAccount";
            this.tblAccountBindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saleInvoiceToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.managementToolStripMenuItem,
            this.managementToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(769, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saleInvoiceToolStripMenuItem
            // 
            this.saleInvoiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchaseToolStripMenuItem,
            this.purchaseInvoiceReturnToolStripMenuItem,
            this.cashBookToolStripMenuItem1,
            this.saleInvoiceToolStripMenuItem1,
            this.saleInvoiceReturnToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cashBookToolStripMenuItem2,
            this.cashBookToolStripMenuItem,
            this.purchaseInvoiceToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.saleInvoiceToolStripMenuItem.Name = "saleInvoiceToolStripMenuItem";
            this.saleInvoiceToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.saleInvoiceToolStripMenuItem.Text = "Data Entry";
            // 
            // purchaseToolStripMenuItem
            // 
            this.purchaseToolStripMenuItem.Name = "purchaseToolStripMenuItem";
            this.purchaseToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.purchaseToolStripMenuItem.Text = "Purchase Invoice ";
            this.purchaseToolStripMenuItem.Click += new System.EventHandler(this.purchaseToolStripMenuItem_Click);
            // 
            // purchaseInvoiceReturnToolStripMenuItem
            // 
            this.purchaseInvoiceReturnToolStripMenuItem.Name = "purchaseInvoiceReturnToolStripMenuItem";
            this.purchaseInvoiceReturnToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.purchaseInvoiceReturnToolStripMenuItem.Text = "Purchase Invoice Return";
            this.purchaseInvoiceReturnToolStripMenuItem.Click += new System.EventHandler(this.purchaseInvoiceReturnToolStripMenuItem_Click);
            // 
            // cashBookToolStripMenuItem1
            // 
            this.cashBookToolStripMenuItem1.Name = "cashBookToolStripMenuItem1";
            this.cashBookToolStripMenuItem1.Size = new System.Drawing.Size(198, 6);
            this.cashBookToolStripMenuItem1.Click += new System.EventHandler(this.cashBookToolStripMenuItem1_Click);
            // 
            // saleInvoiceToolStripMenuItem1
            // 
            this.saleInvoiceToolStripMenuItem1.Name = "saleInvoiceToolStripMenuItem1";
            this.saleInvoiceToolStripMenuItem1.Size = new System.Drawing.Size(201, 22);
            this.saleInvoiceToolStripMenuItem1.Text = "Sale Invoice";
            this.saleInvoiceToolStripMenuItem1.Click += new System.EventHandler(this.saleInvoiceToolStripMenuItem1_Click);
            // 
            // saleInvoiceReturnToolStripMenuItem
            // 
            this.saleInvoiceReturnToolStripMenuItem.Name = "saleInvoiceReturnToolStripMenuItem";
            this.saleInvoiceReturnToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.saleInvoiceReturnToolStripMenuItem.Text = "Sale Invoice Return";
            this.saleInvoiceReturnToolStripMenuItem.Click += new System.EventHandler(this.saleInvoiceReturnToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 6);
            // 
            // cashBookToolStripMenuItem2
            // 
            this.cashBookToolStripMenuItem2.Name = "cashBookToolStripMenuItem2";
            this.cashBookToolStripMenuItem2.Size = new System.Drawing.Size(201, 22);
            this.cashBookToolStripMenuItem2.Text = "CashBook";
            this.cashBookToolStripMenuItem2.Click += new System.EventHandler(this.cashBookToolStripMenuItem2_Click);
            // 
            // cashBookToolStripMenuItem
            // 
            this.cashBookToolStripMenuItem.Name = "cashBookToolStripMenuItem";
            this.cashBookToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cashBookToolStripMenuItem.Text = "Payment/Receipt";
            this.cashBookToolStripMenuItem.Click += new System.EventHandler(this.cashBookToolStripMenuItem_Click);
            // 
            // purchaseInvoiceToolStripMenuItem
            // 
            this.purchaseInvoiceToolStripMenuItem.Name = "purchaseInvoiceToolStripMenuItem";
            this.purchaseInvoiceToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.purchaseInvoiceToolStripMenuItem.Text = "Item Edit/Remove";
            this.purchaseInvoiceToolStripMenuItem.Click += new System.EventHandler(this.purchaseInvoiceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(198, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemCostQuickSearchToolStripMenuItem1,
            this.itemCostQuickSearchToolStripMenuItem,
            this.saleProfitReportToolStripMenuItem,
            this.toolStripMenuItem2,
            this.purchaseReportToolStripMenuItem,
            this.showToolStripMenuItem,
            this.userAccountReportToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // itemCostQuickSearchToolStripMenuItem1
            // 
            this.itemCostQuickSearchToolStripMenuItem1.Name = "itemCostQuickSearchToolStripMenuItem1";
            this.itemCostQuickSearchToolStripMenuItem1.Size = new System.Drawing.Size(199, 22);
            this.itemCostQuickSearchToolStripMenuItem1.Text = "Item Cost-Quick Search";
            this.itemCostQuickSearchToolStripMenuItem1.Click += new System.EventHandler(this.itemCostQuickSearchToolStripMenuItem1_Click);
            // 
            // itemCostQuickSearchToolStripMenuItem
            // 
            this.itemCostQuickSearchToolStripMenuItem.Name = "itemCostQuickSearchToolStripMenuItem";
            this.itemCostQuickSearchToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.itemCostQuickSearchToolStripMenuItem.Text = "Sale Report";
            this.itemCostQuickSearchToolStripMenuItem.Click += new System.EventHandler(this.itemCostQuickSearchToolStripMenuItem_Click);
            // 
            // saleProfitReportToolStripMenuItem
            // 
            this.saleProfitReportToolStripMenuItem.Name = "saleProfitReportToolStripMenuItem";
            this.saleProfitReportToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saleProfitReportToolStripMenuItem.Text = "Sale Profit Report";
            this.saleProfitReportToolStripMenuItem.Click += new System.EventHandler(this.saleProfitReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // purchaseReportToolStripMenuItem
            // 
            this.purchaseReportToolStripMenuItem.Name = "purchaseReportToolStripMenuItem";
            this.purchaseReportToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.purchaseReportToolStripMenuItem.Text = "Purchase Report";
            this.purchaseReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseReportToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // userAccountReportToolStripMenuItem
            // 
            this.userAccountReportToolStripMenuItem.Name = "userAccountReportToolStripMenuItem";
            this.userAccountReportToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.userAccountReportToolStripMenuItem.Text = "User Account Report";
            this.userAccountReportToolStripMenuItem.Click += new System.EventHandler(this.userAccountReportToolStripMenuItem_Click);
            // 
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.restoreToolStripMenuItem});
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.managementToolStripMenuItem.Text = "Utility";
            this.managementToolStripMenuItem.Click += new System.EventHandler(this.managementToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // managementToolStripMenuItem1
            // 
            this.managementToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEditAccountToolStripMenuItem,
            this.addEditUsersToolStripMenuItem});
            this.managementToolStripMenuItem1.Name = "managementToolStripMenuItem1";
            this.managementToolStripMenuItem1.Size = new System.Drawing.Size(90, 20);
            this.managementToolStripMenuItem1.Text = "Management";
            // 
            // addEditAccountToolStripMenuItem
            // 
            this.addEditAccountToolStripMenuItem.Name = "addEditAccountToolStripMenuItem";
            this.addEditAccountToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addEditAccountToolStripMenuItem.Text = "Add/Edit Account";
            this.addEditAccountToolStripMenuItem.Click += new System.EventHandler(this.addEditAccountToolStripMenuItem_Click);
            // 
            // addEditUsersToolStripMenuItem
            // 
            this.addEditUsersToolStripMenuItem.Name = "addEditUsersToolStripMenuItem";
            this.addEditUsersToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addEditUsersToolStripMenuItem.Text = "Add/Edit Users";
            this.addEditUsersToolStripMenuItem.Click += new System.EventHandler(this.addEditUsersToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "tblAccount";
            reportDataSource1.Value = this.tblAccountBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Mobile_Shop_Management_System.invoice.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(96, 178);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(157, 24);
            this.reportViewer1.TabIndex = 4;
            this.reportViewer1.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(769, 350);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mobile Shop Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblAccountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleInvoiceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addEditAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cashBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator cashBookToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cashBookToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.BindingSource tblAccountBindingSource;
        private DataSet1 DataSet1;
        private System.Windows.Forms.ToolStripMenuItem itemCostQuickSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleProfitReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem purchaseReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemCostQuickSearchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleInvoiceReturnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseInvoiceReturnToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem addEditUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userAccountReportToolStripMenuItem;
    }
}