
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    public partial class BookPackItemLogic
    {
        public void SaveItems(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items)
        {
            foreach(BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            {
                item.Save(db, false);

                
            }
            db.SubmitChanges();
            foreach (BusinessLogicLayer.Entity.PPM.BookPackItem item in items)
            {
                

                foreach (BusinessLogicLayer.Entity.PPM.BookPackItemModel model in item.ItemModels)
                {
                    model.BookPackItemID = item.BookPackItemID;
                    model.Save(db, false);
                }
            }
            db.SubmitChanges();
        }
    }
}
      