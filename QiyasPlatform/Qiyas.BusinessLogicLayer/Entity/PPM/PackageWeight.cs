
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class PackageWeight
    {

        public PackageWeight(string Item)
        {
            var pw = context.PackageWeightViews.Where(x => x.PackCode == Item).FirstOrDefault();
            if(pw != null)
            {
                this.entity = context.PackageWeights.Where(p => p.PackageWeightID == pw.PackageWeightID).FirstOrDefault();
            }
            
        }

        private string _PackCode;
        public string PackCode
        {
            set { _PackCode = value; }
            get { return _PackCode; }
        }
        
        private int _Serial;
        public int Serial
        {
            set { _Serial = value; }
            get { return _Serial; }
        }
    }
}
      