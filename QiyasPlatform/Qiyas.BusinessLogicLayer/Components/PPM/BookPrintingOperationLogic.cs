
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
    public partial class BookPrintingOperationLogic
    {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPrintingOperation> GetAllReadyToReceive()
        {
            return db.BookPrintingOperations.Where(c=> c.OperationStatusID == 4 ).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.BookPrintingOperation(c) { context = db }).ToList();
        }

        public bool HasDependencies(int BookPrintingOperationID)
        {
            return false;
        }


        
    }
}
      