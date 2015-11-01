
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
            DataAccessLayer.QiyasLinqDataContext context = new DataAccessLayer.QiyasLinqDataContext();
            foreach(BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            {
                item.Save(db, false);
                context = item.context;
                
            }

            
            db.SubmitChanges();
            List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> itemModels = new List<Entity.PPM.BookPackItemModel>();
            foreach (BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            {
                foreach (BusinessLogicLayer.Entity.PPM.BookPackItemModel model in item.ItemModels)
                {
                    model.BookPackItemID = item.BookPackItemID;
                    itemModels.Add(model);
                    //model.Save(db, false);
                }
            }
            //DataTableHelper.BulkCopyToDatabase(itemModels, "PPM.BookPackItemModel", context);
            db.SubmitChanges();
        }

        public void SaveItemsOptimized(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items)
        {
            
            List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> itemModels = new List<Entity.PPM.BookPackItemModel>();
            //foreach (BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            //{
            //    item.Save(db, false);
            //}

            foreach (BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            {
                foreach (BusinessLogicLayer.Entity.PPM.BookPackItemModel model in item.ItemModels)
                {
                    //model.Save(db, false);
                    model.BookPackItemID = item.BookPackItemID;
                    //model.BookPackItemModelID
                    itemModels.Add(model);
                    //model.Save(db, false);
                }
            }
            DataTableHelper.SaveBulkItemsList(items, itemModels, "PPM.BookPackItem", "PPM.BookPackItemModel");
            
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

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItem> GetAllByPrintingIDForPrinting(int ID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItem> packItems = new List<Entity.PPM.BookPackItem>();
            var items = (from x in db.ViewBookPackItemPrints where x.BookPrintingOperationID == ID orderby x.ExamModelCount, x.BooksPerPackage descending, x.ChildExamModelCount, x.ChildBooksPerPackage descending, x.ModelandNumber, x.PackSerial select x);
            foreach (var item in items)
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

        public int GetLastBookSerialForRePackedItem(int BookPackItemID)
        {
            int? result = db.GetPackedBookItemLastSerial(BookPackItemID);
            return result.HasValue ? result.Value : 0;
        }

        public int GetPackageBooksCount(int BookPackItemID)
        {
            int? result = db.GetPackageBooksCountByBookPackItemId(BookPackItemID);
            return result.HasValue ? result.Value : 0;
        }
    }
}
      