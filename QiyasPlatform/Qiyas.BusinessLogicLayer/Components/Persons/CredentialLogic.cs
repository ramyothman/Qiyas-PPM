using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Qiyas.BusinessLogicLayer.Components.Persons
{
    public class CredentialLogic : QueryBase
    {
        public Entity.Persons.Credential GetByUserName(string userName)
        {
            try
            {
                Entity.Persons.Credential cred = null;
                var item = (from x in db.Credentials where x.Username == userName select x).FirstOrDefault();
                if (item != null)
                {
                    cred = new Entity.Persons.Credential(item);
                }
                return cred;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        

        public Entity.Persons.Credential GetByEmail(string email)
        {
            try
            {
                Entity.Persons.Credential cred = null;
                var item = (from x in db.Credentials where x.Email == email select x).FirstOrDefault();
                if (item != null)
                {
                    cred = new Entity.Persons.Credential(item);
                }
                return cred;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public List<Qiyas.BusinessLogicLayer.Entity.Persons.Credential> GetAll()
        {
            try
            {
                return db.Credentials.OrderBy(c => c.Username).Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.Credential(c) { context = db }).FromCache(db);
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }
    }
}
