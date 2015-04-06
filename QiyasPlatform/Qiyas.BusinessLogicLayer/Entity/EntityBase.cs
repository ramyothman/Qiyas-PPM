using System.IO;
using Qiyas.DataAccessLayer;
using System;

namespace Qiyas.BusinessLogicLayer.Entity
{
    public abstract class EntityBase<T> : EntityBase 
    { 
        internal T entity;

        public override bool HasObject
        {
            get
            {
                bool result = entity != null;
                return result;
            }
        }
    }

    public abstract class EntityBase
    {
        internal QiyasLinqDataContext context = new QiyasLinqDataContext();
        internal bool isNew;
        public Exception lastException;

        public virtual bool HasObject
        {
            get
            {
                
                return false;
            }
        }
        internal virtual bool? Save(QiyasLinqDataContext context, bool commit) { throw new NotImplementedException(); }
        internal virtual bool? Delete(QiyasLinqDataContext context, bool commit) { throw new NotImplementedException(); }
    }
}
