
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class BookPackItem
    {
        private List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> _ItemModels;
        public List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> ItemModels
        {
            set { _ItemModels = value; }
            get 
            { 
                if(_ItemModels == null)
                {
                    _ItemModels = context.BookPackItemModels.Where(c => c.BookPackItemID == BookPackItemID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemModel(c) { context = this.context }).ToList();
                    if (_ItemModels == null)
                        _ItemModels = new List<BookPackItemModel>();
                }
                return _ItemModels;
            }
        }

    }
}
      