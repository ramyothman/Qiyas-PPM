
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    public partial class BookPackingOperationLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> GetByBookPrintingID(int ID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> operations =  db.BookPackingOperations.Where(c => c.BookPrintingOperationID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation(c) { context = db }).ToList();
            if (operations == null)
                operations = new List<Entity.PPM.BookPackingOperation>();
            return operations;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> GetPackedByBookPrintingID(int ID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> operations = new List<Entity.PPM.BookPackingOperation>();
            var packed = db.GetPackedByPrintingID(ID);
            foreach(var item in packed)
            {
                Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation nitem = new Entity.PPM.BookPackingOperation();
                nitem.AllocatedFrom = item.AllocatedFrom;
                nitem.BookPackingOperationID = item.BookPackingOperationID;
                nitem.BookPrintingOperationID = item.BookPrintingOperationID;
                nitem.CreatedDate = item.CreatedDate;
                nitem.CreatorID = item.CreatorID;
                nitem.ModifiedByID = item.ModifiedByID;
                nitem.ModifiedDate = item.ModifiedDate;
                nitem.Name = item.Name;
                nitem.PackageTotal = item.PackageTotal;
                nitem.PackagingTypeID = item.PackagingTypeID;
                nitem.PackingCalculationTypeID = item.PackingCalculationTypeID;
                nitem.PackingParentID = item.PackingParentID;
                nitem.PackingValue = item.PackingValue;
                nitem.isNew = false;
                operations.Add(nitem);
            }

            return operations;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> GetPackagingTypeByBookPrintingID(int ID)
        {
            var list = (from c in db.BookPackingOperations where c.BookPrintingOperationID == ID select new {c.PackagingTypeID}).ToList();
            var packings = (from x in db.BookPackingOperations where x.BookPrintingOperationID == ID select x);
            var itemsList = from t1 in db.PackagingTypes join t2 in packings on t1.PackagingTypeID equals t2.PackagingTypeID select new { t1.Name, t2 };
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> operations = new List<Entity.PPM.BookPackingOperation>();
            foreach( var item in itemsList)
            {
                var p = (from x in packings where x.BookPackingOperationID == item.t2.BookPackingOperationID select x).FirstOrDefault();
                Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation o = new Entity.PPM.BookPackingOperation(p);
                o.PackagingTypeName = item.Name;
                operations.Add(o);
            }
            if (operations == null)
                operations = new List<Entity.PPM.BookPackingOperation>();
            return operations;
        }

        public bool ContainPackType(int ID, int PackagingTypeID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> operations = db.BookPackingOperations.Where(c => c.BookPrintingOperationID == ID && c.PackagingTypeID == PackagingTypeID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation(c) { context = db }).ToList();
            return operations.Count > 0;
            
        }

        public bool ContainPackTypeExclExisting(int ID, int PackagingTypeID, int oldPackagingOperationID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> operations = db.BookPackingOperations.Where(c => c.BookPrintingOperationID == ID && c.PackagingTypeID == PackagingTypeID && c.BookPackingOperationID != oldPackagingOperationID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation(c) { context = db }).ToList();
            return operations.Count > 0;
        }

        public int GetTotal(int ID)
        {
            var result = db.GetTotalPrintingPackageByPrintingID(ID);
            return result.HasValue? result.Value : 0;
        }

        public int GetTotalA3(int ID)
        {
            var result = db.GetTotalPrintingPackageA3ByPrintingID(ID);
            return result.HasValue ? result.Value : 0;
        }

        public int GetTotalItems(int ID)
        {
            var result = db.GetTotalPrintingPackageItemsByPrintingID(ID);
            return result.HasValue ? result.Value : 0;
        }

        public int GetTotalItemsA3(int ID)
        {
            var result = db.GetTotalPrintingPackageItemsA3ByPrintingID(ID);
            return result.HasValue ? result.Value : 0;
        }

        public void DeletePacksByPrintingID(int ID)
        {
            db.DeleteBookPackingOperationByBookPrintingID(ID);
        }

        public int GetLastPackSerial(int ExamID)
        {
            return db.GetLastPackSerialForExamID(ExamID).Value;
        }

        public bool HaveA3Packs(int ID)
        {
            bool? value = db.HaveA3Packs(ID);
            return value.HasValue? value.Value: false;
        }
    }
}
      