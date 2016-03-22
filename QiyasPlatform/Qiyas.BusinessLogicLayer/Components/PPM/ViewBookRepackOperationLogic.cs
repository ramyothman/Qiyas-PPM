using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    [DataObject(true)]
    public class ViewBookRepackOperationLogic : QueryBase
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ViewBookRepackOperation> GetAll(int id)
        {
            return db.ViewBookRepackOperations.Where(x => x.BookPrintingOperationID == id).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ViewBookRepackOperation(c) { context = db }).ToList();
        }
    }
}
