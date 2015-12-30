using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Qiyas.WebAdmin.Common.Reports
{
    public partial class ShippingReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ShippingReport()
        {
            InitializeComponent();
        }

        BusinessLogicLayer.Components.PPM.ShippingBagLogic ShippingBagLogic = new BusinessLogicLayer.Components.PPM.ShippingBagLogic();
        string Supervisor;
        public void LoadData(int ID, string Supervisor)
        {
            this.DataSource = ShippingBagLogic.GetAllShippingBagReportByExamCenterRequiredExamsID(ID);
            this.Supervisor = Supervisor;
        }

        public bool HasData()
        {
            var items = this.DataSource as List<BusinessLogicLayer.Entity.Reports.ShippingBagReport>;
            return items == null ? items.Count > 0 : false;
        }

        private void xrLabelExamPeriodName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel cell = sender as XRLabel;
            if (cell == null || string.IsNullOrEmpty(cell.Text))
                return;

            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam exCenter = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(Convert.ToInt32(cell.Text));
            if (exCenter == null)
                return;
            BusinessLogicLayer.Entity.PPM.ExamPeriod ExamPeriod = new BusinessLogicLayer.Entity.PPM.ExamPeriod(exCenter.ExamPeriodID.Value);
            if (ExamPeriod == null)
                return;

            cell.Text = ExamPeriod.Name;
        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel cell = sender as XRLabel;
            if (cell == null || string.IsNullOrEmpty(cell.Text))
                return;

            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam exCenter = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(Convert.ToInt32(cell.Text));
            if (exCenter == null)
                return;
            BusinessLogicLayer.Entity.PPM.ExamCenter ExamCenter = new BusinessLogicLayer.Entity.PPM.ExamCenter(exCenter.ExamCenterID.Value);
            if (ExamCenter == null)
                return;

            cell.Text = ExamCenter.CenterCode;
        }

        private void xrTableCellSupervisor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = Supervisor;
        }
        
    }
}
