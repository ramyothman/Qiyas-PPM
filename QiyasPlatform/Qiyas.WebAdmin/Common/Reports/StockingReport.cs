using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Qiyas.WebAdmin.Common.Reports
{
    public partial class StockingReport : DevExpress.XtraReports.UI.XtraReport
    {
        public StockingReport()
        {
            InitializeComponent();
        }
        BusinessLogicLayer.Components.PPM.ShippingBagLogic ShippingBagLogic = new BusinessLogicLayer.Components.PPM.ShippingBagLogic();
        List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamModel> ExamModels = new List<BusinessLogicLayer.Entity.PPM.ExamModel>();
        List<string> ExamCodesLoaded = new List<string>();
        int LastSet = 0;
        public void LoadData(int ReportID)
        {
            var bagList = ShippingBagLogic.GetAllShippingBagReportByExamCenterRequiredExamsID(ReportID);
            
            this.DataSource = bagList;
        }

        private void xrTableCellDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string hijri = Common.Helpers.Tools.GregToHijriMonthandYear(DateTime.Now);
            xrTableCellDate.Text = hijri;
        }

        private void xrTableCellModel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            string tagValue = cell.Tag == null? "" : cell.Tag.ToString();
            
            if(!ExamCodesLoaded.Contains(cell.Text))
            {
                var models = ShippingBagLogic.GetModels(cell.Text);
                ExamCodesLoaded.Add(cell.Text);
                ExamModels.Clear();
                foreach(var model in models)
                {
                    ExamModels.Add(model);
                }
                LastSet = ExamModels.Count - 1;
            }

            
            if(ExamModels.Count > 0)
            {
                cell.Tag = "True";
                cell.Text = ExamModels[0].Name;
            }
            else
            {
                cell.Text = "";
            }
        }

        private void xrTableCellModel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            string tagValue = cell.Tag == null ? "" : cell.Tag.ToString();

            if (!ExamCodesLoaded.Contains(cell.Text))
            {
                var models = ShippingBagLogic.GetModels(cell.Text);
                ExamCodesLoaded.Add(cell.Text);
                ExamModels.Clear();
                foreach (var model in models)
                {
                    ExamModels.Add(model);
                }
                LastSet = ExamModels.Count - 1;
            }


            if (ExamModels.Count > 1)
            {
                cell.Tag = "True";
                cell.Text = ExamModels[1].Name;
            }
            else
            {
                cell.Text = "";
            }
        }

        private void xrTableCellModel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            string tagValue = cell.Tag == null ? "" : cell.Tag.ToString();

            if (!ExamCodesLoaded.Contains(cell.Text))
            {
                var models = ShippingBagLogic.GetModels(cell.Text);
                ExamCodesLoaded.Add(cell.Text);
                ExamModels.Clear();
                foreach (var model in models)
                {
                    ExamModels.Add(model);
                }
                LastSet = ExamModels.Count - 1;
            }


            if (ExamModels.Count > 2)
            {
                cell.Tag = "True";
                cell.Text = ExamModels[2].Name;
            }
            else
            {
                cell.Text = "";
            }
        }

        private void xrTableCellModel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            string tagValue = cell.Tag == null ? "" : cell.Tag.ToString();

            if (!ExamCodesLoaded.Contains(cell.Text))
            {
                var models = ShippingBagLogic.GetModels(cell.Text);
                ExamCodesLoaded.Add(cell.Text);
                ExamModels.Clear();
                foreach (var model in models)
                {
                    ExamModels.Add(model);
                }
                LastSet = ExamModels.Count - 1;
            }


            if ( ExamModels.Count > 3)
            {
                cell.Tag = "True";
                cell.Text = ExamModels[3].Name;
            }
            else
            {
                cell.Text = "";
            }
        }

        private void xrTableCellModel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            string tagValue = cell.Tag == null ? "" : cell.Tag.ToString();

            if (!ExamCodesLoaded.Contains(cell.Text))
            {
                var models = ShippingBagLogic.GetModels(cell.Text);
                ExamCodesLoaded.Add(cell.Text);
                ExamModels.Clear();
                foreach (var model in models)
                {
                    ExamModels.Add(model);
                }
                LastSet = ExamModels.Count - 1;
            }


            if ( ExamModels.Count > 4)
            {
                cell.Tag = "True";
                cell.Text = ExamModels[4].Name;
            }
            else
            {
                cell.Text = "";
            }
        }

        private void xrTableCellCityandCenter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if(cell == null)
                return;
            int id = 0;
            if (string.IsNullOrEmpty(cell.Text))
                return;
            Int32.TryParse(cell.Text, out id);
            if (id == 0)
                return;
            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam exCenter = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(id);
            if (exCenter == null)
                return;
            BusinessLogicLayer.Entity.PPM.ExamCenter center = new BusinessLogicLayer.Entity.PPM.ExamCenter(exCenter.ExamCenterID.Value);
            if (center == null)
                return;
            BusinessLogicLayer.Entity.Persons.City city = new BusinessLogicLayer.Entity.Persons.City(center.CityID.Value);
            if(city == null)
                return;

            cell.Text = city.Name + " - " + center.Name;
            //BusinessLogicLayer.Entity.PPM.ExamPeriod gender = new BusinessLogicLayer.Entity.PPM.ExamPeriod(exCenter.ExamPeriodID.Value);
            //if (gender == null)
            //    return;
        }

        private void xrTableCellPeriodName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null)
                return;
            int id = 0;
            if (string.IsNullOrEmpty(cell.Text))
                return;
            Int32.TryParse(cell.Text, out id);
            if (id == 0)
                return;
            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam exCenter = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(id);
            if (exCenter == null)
                return;

            BusinessLogicLayer.Entity.PPM.ExamPeriod period = new BusinessLogicLayer.Entity.PPM.ExamPeriod(exCenter.ExamPeriodID.Value);
            if (period == null)
                return;

            cell.Text = period.Name;
        }

        private void xrLabelGender_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
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
    }
}
