
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
    [DataObject(true)]
    public partial class BookPackingOperationLogic : QueryBase
    {
        #region Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> GetAll()
        {
            return db.BookPackingOperations.Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation(c) { context = db }).ToList();
        }
        #endregion
    }
}
      