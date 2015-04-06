using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qiyas.DataAccessLayer;

namespace Qiyas.BusinessLogicLayer.Components
{
    public abstract class QueryBase
    {
        protected QiyasLinqDataContext db = new QiyasLinqDataContext();
        public Exception lastException = null;
    }
}
