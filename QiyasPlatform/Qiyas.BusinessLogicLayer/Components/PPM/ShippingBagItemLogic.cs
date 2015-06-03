
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
    public partial class ShippingBagItemLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBagItem> GetAll(int ID)
        {
            
            return db.ViewShippingBagItems.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBagItem() { context = db, BookPackItemID = c.BookPackItemID, BooksCount = c.BooksCount, CreatedBy = c.CreatedBy, CreatedDate = c.CreatedDate, ExamCenterCode = c.ExamCode, ExamModelName = c.ExamModelName, ModifiedBy = c.ModifiedBy, ModifiedDate = c.ModifiedDate, PackageCode = c.PackCode, PackSerial = c.PackSerial, ShippingBagID = c.ShippingBagID, ShippingBagItemID = c.ShippingBagItemID, TotalPacks = c.TotalPacks }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBagItem> GetAll(int ID, int ShippingBagID)
        {
            return db.ViewShippingBagItems.Where(c => c.ExamCenterRequiredExamsID == ID && c.ShippingBagID == ShippingBagID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBagItem() { context = db, BookPackItemID = c.BookPackItemID, BooksCount = c.BooksCount, CreatedBy = c.CreatedBy, CreatedDate = c.CreatedDate, ExamCenterCode = c.ExamCode, ExamModelName = c.ExamModelName, ModifiedBy = c.ModifiedBy, ModifiedDate = c.ModifiedDate, PackageCode = c.PackCode, PackSerial = c.PackSerial, ShippingBagID = c.ShippingBagID, ShippingBagItemID = c.ShippingBagItemID, TotalPacks = c.TotalPacks }).ToList();
        }
    }
}
      