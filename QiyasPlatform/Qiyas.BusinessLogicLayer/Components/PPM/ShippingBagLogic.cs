
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    public partial class ShippingBagLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBag> GetAllByExamCenterRequiredExamsID(int ID)
        {
            return db.ShippingBags.Where(c=> c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBag(c) { context = db }).ToList();
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Reports.ShippingBagReport> GetAllShippingBagReport(int ID)
        {
            return db.ViewShippingBagsReports.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.Reports.ShippingBagReport() { context = db, BooksTotal = c.BooksTotal.Value, ExamCenterCode = c.CenterCode , ExamCenterName = c.CenterName, ExamCode = c.ExamCode, ModelCode = c.ModelName, NumberofPacks = c.NumberofPacks.Value, PackagingType = c.PackagingName, PacksSerials =c.PackSerials, ShippingBagNumber = c.ShippingBagSerial.Value}).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Reports.ShippingBagReport> GetAllShippingBagReportByCenterID(int ID)
        {
            return db.ViewShippingBagsReports.Where(c => c.ExaminationCenterID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.Reports.ShippingBagReport() { context = db, BooksTotal = c.BooksTotal.Value, ExamCenterCode = c.CenterCode, ExamCenterName = c.CenterName, ExamCode = c.ExamCode, ModelCode = c.ModelName, NumberofPacks = c.NumberofPacks.Value, PackagingType = c.PackagingName, PacksSerials = c.PackSerials, ShippingBagNumber = c.ShippingBagSerial.Value }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Reports.ShippingBagReport> GetAllShippingBagReportByExamCenterRequiredExamsID(int ID)
        {
            return db.ViewShippingBagsReports.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.Reports.ShippingBagReport() { context = db, BooksTotal = c.BooksTotal.Value, ExamCenterCode = c.CenterCode, ExamCenterName = c.CenterName, ExamCode = c.ExamCode, ModelCode = c.ModelName, NumberofPacks = c.NumberofPacks.Value, PackagingType = c.PackagingName, PacksSerials = c.PackSerials, ShippingBagNumber = c.ShippingBagSerial.Value }).ToList();
        }
    }
}
      