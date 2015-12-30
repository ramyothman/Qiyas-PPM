using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Qiyas.WebAdmin.Common.Reports
{
    public partial class WithdrawReport : DevExpress.XtraReports.UI.XtraReport
    {
        BusinessLogicLayer.Components.PPM.RequestWithdrawDetailLogic RequestWithdrawDetailLogic = new BusinessLogicLayer.Components.PPM.RequestWithdrawDetailLogic();

        public void LoadData(int ID, string reportType, string Supervisor)
        {
            this.Supervisor = Supervisor;
            switch(reportType)
            {
                case "withdraw":
                    LoadDataByWithdrawID(ID);
                    break;
                case "request":
                    LoadDataByRequestID(ID);
                    break;
            }
        }

        public bool HasData()
        {
            var items = this.DataSource as List<BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail>;
            return items == null ? items.Count > 0 : false;
        }
        public void LoadDataByWithdrawID(int ID)
        {
            this.DataSource = RequestWithdrawDetailLogic.ViewWithdrawalReportByRequestWithdrawID(ID);
        }

        public void LoadDataByRequestID(int ID)
        {
            this.DataSource = RequestWithdrawDetailLogic.ViewWithdrawalReportByExamCenterRequiredExamsID(ID);
        }

        public WithdrawReport()
        {
            InitializeComponent();
        }

        private void xrLabelExamCenter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrLabelGender_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel cell = sender as XRLabel;
            if (cell == null)
                return;

            BusinessLogicLayer.Entity.PPM.Exam exCenter = new BusinessLogicLayer.Entity.PPM.Exam(cell.Text);
            if (exCenter == null)
                return;
            BusinessLogicLayer.Entity.PPM.StudentGender gender = new BusinessLogicLayer.Entity.PPM.StudentGender(exCenter.StudentGenderID.Value);
            if (gender == null)
                return;

            cell.Text = gender.Name;
        }
        string Supervisor;
        private void xrTableCellSupervisor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = Supervisor;
        }

    }
}
