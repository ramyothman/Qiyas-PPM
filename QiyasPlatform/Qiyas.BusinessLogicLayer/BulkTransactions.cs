using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qiyas.BusinessLogicLayer.Entity;
using Qiyas.DataAccessLayer;

namespace Qiyas.BusinessLogicLayer
{
    public class BulkTransactions
    {
        private QiyasLinqDataContext db = new QiyasLinqDataContext();
        public Exception lastException;

        public bool Save<T>(List<T> list) where T : EntityBase
        {
            var newItems = list.Where(e => e.isNew).ToList();
            var oldItems = list.Where(e => !e.isNew).ToList();
            try
            {
                if (newItems.Count() > 0)
                {
                    foreach (var item in newItems)
                    {
                        var attached = item.Save(db, false);
                        if (attached.HasValue && attached == false)
                            throw new Exception("item has problem with attaching");
                        item.context = db;
                    }
                    db.SubmitChanges();
                    newItems.ToList().ForEach(i => i.isNew = false);
                }
                if (oldItems.Count() > 0)
                {
                    foreach (var item in oldItems)
                    {
                        var attached = item.Save(db, false);
                        if (attached.HasValue && attached == false)
                            throw new Exception("item has problem with attaching");
                    }
                    oldItems.Select(i => i.context).Distinct().ToList().ForEach(c => c.SubmitChanges());
                }
                return true;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return false;
            }
        }

        public bool Delete<T>(List<T> list) where T : EntityBase
        {
            try
            {
                foreach (var item in list)
                {
                    var attached = item.Delete(item.context, false);
                    if (attached.HasValue && attached == false)
                        throw new Exception("item has problem with attaching (Are you sure that you sent NON isNew item ?");
                }
                list.Select(i => i.context).Distinct().ToList().ForEach(c => c.SubmitChanges());
                return true;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return false;
            }
        }
    }
}
