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

        public void LoadData(int ID)
        {
            this.DataSource = ShippingBagLogic.GetAllShippingBagReportByExamCenterRequiredExamsID(ID);
        }

        public bool HasData()
        {
            var items = this.DataSource as List<BusinessLogicLayer.Entity.Reports.ShippingBagReport>;
            return items == null ? items.Count > 0 : false;
        }
        
    }
}
