
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class ShippingBag
    {
        public ShippingBag(int ID, int ShippingBagSerial)
        {
            this.entity = context.ShippingBags.Where(p => p.ShippingBagSerial == ShippingBagSerial && p.ExamCenterRequiredExamsID == ID).FirstOrDefault();
        }

        private string _ExamCode;
        public string ExamCode
        {
            set { _ExamCode = value; }
            get { return _ExamCode; }
        }

        private string _CenterFullName;
        public string CenterFullName
        {
            set { _CenterFullName = value; }
            get { return _CenterFullName; }
        }
    }
}
      