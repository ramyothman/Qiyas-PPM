
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class ShippingBagItem
    {

        public ShippingBagItem(int ShippingBagID, int BookPackItemID)
        {
            this.entity = context.ShippingBagItems.Where(p => p.ShippingBagID == ShippingBagID && p.BookPackItemID == BookPackItemID).FirstOrDefault();
        }
        private string _PackageCode;
        [Display(Name = "PackageCode")]
        public string PackageCode
        {
            set { _PackageCode = value; }
            get { return _PackageCode; }
        }

        private string _ExamCenterCode;
        [Display(Name = "CenterCode")]
        public string ExamCenterCode
        {
            set { _ExamCenterCode = value; }
            get
            {
                return _ExamCenterCode;
            }
        }

        private string _ExamModelName;
        [Display(Name = "ExamModelName")]
        public string ExamModelName
        {
            set { _ExamModelName = value; }
            get { return _ExamModelName; }
        }

        private int? _TotalPacks;
        [Display(Name = "TotalPacks")]
        public int? TotalPacks
        {
            set { _TotalPacks = value; }
            get { return _TotalPacks; }
        }

        private int? _BooksCount;
        [Display(Name = "BookCount")]
        public int? BooksCount
        {
            set { _BooksCount = value; }
            get { return _BooksCount; }
        }

        private int? _PackSerial;
        [Display(Name = "PackSerial")]
        public int? PackSerial
        {
            set { _PackSerial = value; }
            get { return _PackSerial; }
        }


    }
}
      