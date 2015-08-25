using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qiyas.BusinessLogicLayer.Entity.Reports
{
    public class ShippingBagReport : EntityBase
    {
        public string ExamCenterCode { set; get; }
        public string ExamCenterName { set; get; }
        public string ExamCode { set; get; }
        public string ModelCode { set; get; }
        public string PackagingType { set; get; }
        public int NumberofPacks { set; get; }
        public string PacksSerials { set; get; }
        public int BooksTotal { set; get; }
        public int ShippingBagNumber { set; get; }
    }
}
