
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
    public partial class BookPackingOperationLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> GetByBookPrintingID(int ID)
        {
            List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation> operations =  db.BookPackingOperations.Where(c => c.BookPrintingOperationID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation(c) { context = db }).ToList();
            if (operations == null)
                operations = new List<Entity.PPM.BookPackingOperation>();
            return operations;
        }     
    }
}
      