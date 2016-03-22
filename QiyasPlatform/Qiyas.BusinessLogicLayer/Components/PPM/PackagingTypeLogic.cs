
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
    public partial class PackagingTypeLogic
    {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType> GetAllActive()
        {
            return db.PackagingTypes.Where(c => c.IsActive == true).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType(c) { context = db }).ToList();
        }

        

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType> GetAllForPrint(int ID)
        {
            BusinessLogicLayer.Entity.PPM.BookPrintingOperation type = new Entity.PPM.BookPrintingOperation(ID);
            BusinessLogicLayer.Components.PPM.ExamLogic logic = new ExamLogic();

            int count = logic.GetExamModelCount(type.ExamID.Value);
            return db.PackagingTypes.Where(c => c.IsActive == true && (c.ExamModelCount == count || c.ExamModelCount == 1)).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType(c) { context = db }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType> GetAllForPrintByCount(int count)
        {
            return db.PackagingTypes.Where(c => c.IsActive == true && (c.ExamModelCount == count || c.ExamModelCount == 1)).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType(c) { context = db }).ToList();
        }

        public BusinessLogicLayer.Entity.PPM.PackagingType GetByBookCountandExamModelCount(int BookCount, int ExamModelCount)
        {
            BusinessLogicLayer.Entity.PPM.PackagingType package = null;
            var pexist = db.PackagingTypes.Where(p => p.BooksPerPackage == BookCount && p.ExamModelCount == ExamModelCount).FirstOrDefault();
            if (pexist != null)
                package = new Entity.PPM.PackagingType(pexist);
            return package;
        }

        public bool HasDependencies(int PackagingTypeID)
        {
            return false;
        }
    }
}
      