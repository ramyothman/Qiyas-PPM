
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class PackagingType
    {
        public PackagingType(int ExamModelCount, int BooksPerPackage)
        {
            this.entity = context.PackagingTypes.Where(p => p.ExamModelCount == ExamModelCount && p.BooksPerPackage == BooksPerPackage).FirstOrDefault();
        }
    }
}
      