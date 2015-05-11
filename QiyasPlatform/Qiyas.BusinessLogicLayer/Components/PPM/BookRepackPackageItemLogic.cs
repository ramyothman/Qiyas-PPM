using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    public partial class BookRepackPackageItemLogic
    {
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookRepackPackageItem> GetAllByBookRepackPackageID(int ID)
        {
            return db.ViewRepackPackageItems.Where(c => c.BookRepackPackageID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookRepackPackageItem() { context = db, BookPackItemID = c.BookPackItemID, BookRepackPackageID = c.BookRepackPackageID, BookRepackPackageItemID = c.BookRepackPackageItemID, ExamCode = c.ExamCode, ExamModelName = c.ExamModelName, PackCode = c.PackCode, PackSerial = c.PackSerial.Value, BooksCount = c.BooksCount.Value, PrintsForOneModel = c.PrintsForOneModel, ExamsNeededForA3 = c.ExamsNeededForA3, TotalExamModels = c.TotalExamModels, PackageTypeBooksPerPackage = c.PackageTypeBooksPerPackage, PackageTypeExamModelCount = c.PackageTypeExamModelCount, PackageTypeTotal = c.PackageTypeTotal }).ToList();
        }

        public Qiyas.BusinessLogicLayer.Entity.PPM.BookRepackPackageItem GetBookRepackItem(string packCode)
        {
            Qiyas.BusinessLogicLayer.Entity.PPM.BookRepackPackageItem item = new Entity.PPM.BookRepackPackageItem();
            var pack = db.ViewBookPackItemCompletes.Where(c => c.PackCode == packCode).FirstOrDefault();
            item.BookPackItemID = pack.BookPackItemID;
            item.ExamCode = pack.ExamCode;
            item.ExamModelName = pack.ExamModelName;
            item.PackCode = pack.PackCode;
            item.PackSerial = pack.PackSerial;
            item.TotalPacks = pack.TotalPacks;
            item.BooksCount = pack.BooksCount;
            item.ExamID = pack.ExamID;
            item.PrintsForOneModel = pack.PrintsForOneModel;
            item.ExamsNeededForA3 = pack.ExamsNeededForA3;
            item.TotalExamModels = pack.TotalExamModels;
            item.PackageTypeBooksPerPackage = pack.PackageTypeBooksPerPackage;
            item.PackageTypeExamModelCount = pack.PackageTypeExamModelCount;
            item.PackageTypeTotal = pack.PackageTypeTotal;
            return item;
        }

        public void RemoveFromList(List<Qiyas.BusinessLogicLayer.Entity.PPM.BookRepackPackageItem> items, int BookPackItemID)
        {
            var item = items.Where(c => c.BookPackItemID == BookPackItemID).FirstOrDefault();
            if(item != null)
                items.Remove(item);
        }
    }
}
