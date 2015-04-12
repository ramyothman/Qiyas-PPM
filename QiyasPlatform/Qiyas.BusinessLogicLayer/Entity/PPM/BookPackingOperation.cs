
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class BookPackingOperation
    {
        string _PackagingTypeName = "";
        [Display(Name = "PackagingTypeName")]
        public string PackagingTypeName
        {
            set { this._PackagingTypeName = value; }
            get { return this._PackagingTypeName; }
        }
    }
}
      