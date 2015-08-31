
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
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBag> GetAllViewByExamCenterRequiredExamsID(int ID)
        {
            var items = db.ViewShippingBags.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBag() { context = db, BookCount = c.BookCount, ExamCenterRequiredExamsID = c.ExamCenterRequiredExamsID, ExamCode = c.ExamCode, PackCount = c.PackCount, ShippingBagCode = c.ShippingBagCode, ShippingBagID = c.ShippingBagID, ShippingBagSerial = c.ShippingBagSerial, CenterFullName = c.CenterCode + "-" + c.CenterName }).ToList();
            List<Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBag> itemsDistinct = new List<Entity.PPM.ShippingBag>();
            foreach(var item in items)
            {
                var exists = (from x in itemsDistinct where x.ShippingBagID == item.ShippingBagID select x).FirstOrDefault();
                if(exists ==null)
                {
                    itemsDistinct.Add(item);
                }
            }
            
            return itemsDistinct;
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
            return db.ViewShippingBagsReports.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.Reports.ShippingBagReport() { context = db, BooksTotal = c.BooksTotal.Value, ExamCenterCode = c.CenterCode, ExamCenterName = c.CenterName, ExamCode = c.ExamCode, ModelCode = c.ModelName, NumberofPacks = c.NumberofPacks.Value, PackagingType = c.PackagingName, PacksSerials = c.PackSerials, ShippingBagNumber = c.ShippingBagSerial.Value, ExamCenterRequiredExamsID = c.ExamCenterRequiredExamsID.Value }).ToList();
        }

        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamModel> GetModels(string ExamCode)
        {
            var items = db.ViewExamModels.Where(c=> c.ExamCode == ExamCode).Select(d => new BusinessLogicLayer.Entity.PPM.ExamModel(){ context = db, ExamModelID = d.ExamModelID.Value, Name = d.ExamModel }).ToList();
            return items;
        }
    }
}
      