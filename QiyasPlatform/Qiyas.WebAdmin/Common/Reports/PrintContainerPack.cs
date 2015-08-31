using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Qiyas.WebAdmin.Common.Reports
{
    public partial class PrintContainerPack : DevExpress.XtraReports.UI.XtraReport
    {
        public PrintContainerPack()
        {
            InitializeComponent();
        }

        public void LoadData(int ID)
        {
            this.DataSource = new BusinessLogicLayer.Components.PPM.ShippingBagLogic().GetAllViewByExamCenterRequiredExamsID(ID);
        }

    }
}
