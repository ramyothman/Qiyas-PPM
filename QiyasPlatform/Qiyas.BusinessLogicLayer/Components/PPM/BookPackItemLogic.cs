
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
    public partial class BookPackItemLogic
    {
        public void SaveItems(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items)
        {
            foreach(BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            {
                item.Save(db, false);

                
            }
            db.SubmitChanges();
            foreach (BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            {
                

                foreach (BusinessLogicLayer.Entity.PPM.BookPackItemModel model in item.ItemModels)
                {
                    model.BookPackItemID = item.BookPackItemID;
                    model.Save(db, false);
                }
            }
            db.SubmitChanges();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItem> GetAllByPrintingID(int ID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItem> packItems = new List<Entity.PPM.BookPackItem>();
            var items = (from x in db.ViewBookPackItemPrints where x.BookPrintingOperationID == ID select x);
            foreach(var item in items)
            {
                Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItem packItem = new Entity.PPM.BookPackItem();
                packItem.BookPackingOperationID = item.BookPackingOperationID;
                packItem.BookPackItemID = item.BookPackItemID;
                packItem.isNew = false;
                packItem.ModelandNumber = item.ModelandNumber;
                packItem.OperationStatusID = item.OperationStatusID;
                packItem.PackCode = item.PackCode;
                packItem.PackageTypeName = item.PackageTypeName;
                packItem.StartBookSerial = item.StartBookSerial;
                packItem.LastBookSerial = item.LastBookSerial;
                packItem.PackSerial = item.PackSerial;
                packItem.ParentID = item.ParentID;
                packItem.Speciality = item.Speciality;
                packItem.Weight = item.Weight;
                packItems.Add(packItem);
            }
            return packItems;
        }

        public object GetAllByParentID(int ID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItem> packItems = new List<Entity.PPM.BookPackItem>();
            var items = (from x in db.ViewBookPackItemPrintSubs where x.ParentID == ID select x);
            foreach (var item in items)
            {
                Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItem packItem = new Entity.PPM.BookPackItem();
                //packItem.BookPackingOperationID = item.BookPackingOperationID;
                packItem.BookPackItemID = item.BookPackItemID;
                packItem.isNew = false;
                packItem.ModelandNumber = item.ModelandNumber;
                packItem.OperationStatusID = item.OperationStatusID;
                packItem.PackCode = item.PackCode;
                packItem.PackageTypeName = item.PackageTypeName;
                packItem.StartBookSerial = item.StartBookSerial;
                packItem.LastBookSerial = item.LastBookSerial;
                packItem.PackSerial = item.PackSerial;
                packItem.ParentID = item.ParentID;
                packItem.Speciality = item.Speciality;
                packItem.Weight = item.Weight;
                packItems.Add(packItem);
            }
            return packItems;
        }
    }
}
      