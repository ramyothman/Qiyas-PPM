
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
      