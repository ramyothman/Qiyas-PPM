using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
namespace Qiyas.BusinessLogicLayer.Components.RoleSecurity
{
    public class UserLoginProviderLogic : QueryBase
    {


        public List<Entity.RoleSecurity.UserLoginProvider> GetByUserId(int userId)
        {
            try
            {
                var items = db.UserLoginProviders.Where(x => x.UserId == userId).Select(c => new Qiyas.BusinessLogicLayer.Entity.RoleSecurity.UserLoginProvider(c) { context = db }).ToList();
                return items;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public string GetUserIdByLogin(UserLoginInfo userLogin)
        {
            var item = (from x in db.UserLoginProviders where x.LoginProvider == userLogin.LoginProvider && x.ProviderKey == userLogin.ProviderKey select x).FirstOrDefault();
            return item.UserId.ToString();
        }

        public List<UserLoginInfo> GetUserLoginsInfoByUserId(int userId)
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            var items = GetByUserId(userId);
            foreach (BusinessLogicLayer.Entity.RoleSecurity.UserLoginProvider c in items)
            {
                UserLoginInfo login = new UserLoginInfo(c.LoginProvider, c.ProviderKey);
                logins.Add(login);
            }
            return logins;
        }

        public List<Qiyas.BusinessLogicLayer.Entity.RoleSecurity.UserLoginProvider> GetAll()
        {
            try
            {
                return db.UserLoginProviders.OrderBy(c => c.UserId).Select(c => new Qiyas.BusinessLogicLayer.Entity.RoleSecurity.UserLoginProvider(c) { context = db }).ToList();
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public void DeleteByUserId(int userID)
        {
            var logins = (from x in db.UserLoginProviders where x.UserId == userID select x);

            foreach (DataAccessLayer.UserLoginProvider login in logins)
            {
                db.UserLoginProviders.DeleteOnSubmit(login);
            }
            db.SubmitChanges();
        }
    }
}
