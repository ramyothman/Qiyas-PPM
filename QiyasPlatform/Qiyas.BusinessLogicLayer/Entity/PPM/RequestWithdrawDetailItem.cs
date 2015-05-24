
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class RequestWithdrawDetailItem
    {
        private List<BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItemModel> _Models;
        public List<BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItemModel> Models
        {
            set { _Models = value; }
            get 
            {
                if (_Models == null)
                    _Models = new List<RequestWithdrawDetailItemModel>();
                return _Models; 
            }
        }
    }
}
      