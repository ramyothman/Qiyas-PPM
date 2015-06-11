
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

        private List<BookPackingOperation> _ChildPackingOperations = new List<BookPackingOperation>();
        public List<BookPackingOperation> ChildPackingOperations
        {
            set { _ChildPackingOperations = value; }
            get { return _ChildPackingOperations; }
        }

        private List<BookPackingOperation> _SingleChildPackingOperations = new List<BookPackingOperation>();
        public List<BookPackingOperation> SingleChildPackingOperations
        {
            set { _SingleChildPackingOperations = value; }
            get { return _SingleChildPackingOperations; }
        }

        private List<BookPackingOperation> _MultiChildPackingOperations = new List<BookPackingOperation>();
        public List<BookPackingOperation> MultiChildPackingOperations
        {
            set { _MultiChildPackingOperations = value; }
            get { return _MultiChildPackingOperations; }
        }
    }
}
      