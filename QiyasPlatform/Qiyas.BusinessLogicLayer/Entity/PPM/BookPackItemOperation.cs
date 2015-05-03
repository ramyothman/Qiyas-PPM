
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class BookPackItemOperation
    {
        private string _PackagingTypeName = "";
        public string PackagingTypeName 
        {
            set { _PackagingTypeName = value; }
            get { return _PackagingTypeName; }
        }
    }
}
      