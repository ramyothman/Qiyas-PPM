
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
    public partial class BookPackItemOperationLogic
    {

        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation> GetByBookPackID(int PackID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation> operations = db.BookPackItemOperations.Where(c => c.BookPackItemID == PackID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation(c) { context = db }).ToList();
            if (operations == null)
                operations = new List<Entity.PPM.BookPackItemOperation>();
            return operations;
        }



        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation> GetPackedByBookPackID(int ID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation> operations = new List<Entity.PPM.BookPackItemOperation>();
            var packed = db.GetItemPackedByPackID(ID);
            foreach (var item in packed)
            {
                Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation nitem = new Entity.PPM.BookPackItemOperation();
                nitem.AllocatedFrom = item.AllocatedFrom;
                nitem.BookPackItemOperationID = item.BookPackItemOperationID;
                nitem.BookPackItemID = item.BookPackItemID;

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
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation> GetPackagingTypeByBookPackID(int ID)
        {
            var list = (from c in db.BookPackItemOperations where c.BookPackItemID == ID select new { c.PackagingTypeID }).ToList();
            var packings = (from x in db.BookPackItemOperations where x.BookPackItemID == ID select x);
            var itemsList = from t1 in db.PackagingTypes join t2 in packings on t1.PackagingTypeID equals t2.PackagingTypeID select new { t1.Name, t2 };
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation> operations = new List<Entity.PPM.BookPackItemOperation>();
            foreach (var item in itemsList)
            {
                var p = (from x in packings where x.BookPackItemOperationID == item.t2.BookPackItemOperationID select x).FirstOrDefault();
                Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation o = new Entity.PPM.BookPackItemOperation(p);
                o.PackagingTypeName = item.Name;
                operations.Add(o);
            }
            if (operations == null)
                operations = new List<Entity.PPM.BookPackItemOperation>();
            return operations;
        }

        public int GetTotal(int ID)
        {
            var result = db.GetTotalPrintingPackageByItemPackID(ID);
            return result.HasValue ? result.Value : 0;
        }

        public int GetTotalA3(int ID)
        {
            var result = db.GetTotalPrintingPackageA3ByItemPackID(ID);
            return result.HasValue ? result.Value : 0;
        }

        public int GetTotalItems(int ID)
        {
            var result = db.GetTotalPrintingPackageByPackID(ID);
            return result.HasValue ? result.Value : 0;
        }

        public int GetTotalItemsA3(int ID)
        {
            var result = db.GetTotalPrintingPackageItemsA3ByPackID(ID);
            return result.HasValue ? result.Value : 0;
        }
    }
}
      