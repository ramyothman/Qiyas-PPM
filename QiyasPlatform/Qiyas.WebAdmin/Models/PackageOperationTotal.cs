using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qiyas.WebAdmin.Models
{
    public class PackageOperationTotal
    {
        public int Total { set; get; }
        public int TotalPerModel { set; get; }
        public int TotalBooksPerModel { set; get; }
        public int TotalA3 { set; get; }

        public int RemainingPerModel { set; get; }
    }
}