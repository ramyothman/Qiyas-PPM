using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Qiyas.BusinessLogicLayer.Components.Persons
{
    public class PersonLogic : QueryBase
    {
        public Entity.Persons.Person GetByUserName(string userName)
        {
            try
            {
                Entity.Persons.Person person = null;
                var item = (from x in db.Credentials where x.Username == userName select x).FirstOrDefault();
                if (item != null)
                {
                    person = new Entity.Persons.Person(item.BusinessEntityId);
                    person.CurrentCredential = new Entity.Persons.Credential(item);
                }
                    
                return person;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public Entity.Persons.Person GetByEmail(string email)
        {
            try
            {
                Entity.Persons.Person person = null;
                var item = (from x in db.Credentials where x.Email == email select x).FirstOrDefault();
                if (item != null)
                {
                    person = new Entity.Persons.Person(item.BusinessEntityId);
                    person.CurrentCredential = new Entity.Persons.Credential(item);
                }

                return person;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public List<Entity.Persons.Person> GetByUserNameList(string userName)
        {
            try
            {
                List<Entity.Persons.Person> persons = new List<Entity.Persons.Person>();
                var items = db.Credentials.Where(x => x.Username == userName).Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.Credential(c) { context = db });
                if (items != null)
                {
                    foreach(Entity.Persons.Credential cred in items)
                    {
                        BusinessLogicLayer.Entity.Persons.Person person = new Entity.Persons.Person(cred.BusinessEntityId);
                        person.CurrentCredential = cred;
                        persons.Add(person);
                    }
                    
                }
                 
                return persons;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public Entity.Persons.Person GetByID(int id)
        {
            try
            {
                Entity.Persons.Person person = null;
                var item = (from x in db.Persons where x.BusinessEntityId == id select x).FirstOrDefault();
                if (item != null)
                {
                    person = new Entity.Persons.Person(item);
                }
                return person;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public List<Qiyas.BusinessLogicLayer.Entity.Persons.Person> GetAll()
        {
            try
            {
                return db.Persons.OrderBy(c => c.FirstName).Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.Person(c) { context = db }).FromCache(db);
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }
    }
}
