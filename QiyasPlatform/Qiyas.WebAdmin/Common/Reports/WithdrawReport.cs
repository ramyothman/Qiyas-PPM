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

        public void LoadData(int ID, string reportType)
        {
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

    }
}
