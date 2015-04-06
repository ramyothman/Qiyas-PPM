
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class ExamCenter
    {
        
        public int? StateProvinceID
        {
            set { this.entity.StateRegionID = value; }
            get
            {
                return this.entity.StateRegionID;
            }
        }

        public string StateProvinceName
        {
            set { this.entity.StateProvinceName = value; }
            get { return this.entity.StateProvinceName; }
        }

        public string CityName
        {
            set { this.entity.CityName = value; }
            get { return this.entity.CityName; }
        }
    }
}
      