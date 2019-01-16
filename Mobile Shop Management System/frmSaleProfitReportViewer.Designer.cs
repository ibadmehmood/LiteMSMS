namespace Mobile_Shop_Management_System
{
    partial class frmSaleProfitReportViewer
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
            this.SaleReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SaleReport = new Mobile_Shop_Management_System.SaleReport();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SaleProfitReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SaleReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleProfitReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SaleReportBindingSource
            // 
            this.SaleReportBindingSource.DataMember = "SaleReport";
            this.SaleReportBindingSource.DataSource = this.SaleReport;
            // 
            // SaleReport
            // 
            this.SaleReport.DataSetName = "SaleReport";
            this.SaleReport.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.AutoSize = true;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "SaleReport";
            reportDataSource1.Value = this.SaleProfitReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Mobile_Shop_Management_System.saleprofitreport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(797, 358);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // SaleProfitReportBindingSource
            // 
            this.SaleProfitReportBindingSource.DataMember = "SaleProfitReport";
            this.SaleProfitReportBindingSource.DataSource = this.SaleReport;
            // 
            // frmSaleProfitReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(797, 358);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmSaleProfitReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSaleReportViewer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSaleReportViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SaleReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaleProfitReportBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SaleReportBindingSource;
        private SaleReport SaleReport;
        private System.Windows.Forms.BindingSource SaleProfitReportBindingSource;
    }
}