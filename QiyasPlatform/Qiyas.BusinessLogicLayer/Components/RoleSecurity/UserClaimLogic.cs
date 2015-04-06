using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Qiyas.BusinessLogicLayer.Components.RoleSecurity
{
    public class UserClaimLogic : QueryBase
    {


        public List<Entity.RoleSecurity.UserClaim> GetByUserId(int userId)
        {
            try
            {
                var items = db.UserClaims.Where(x => x.UserId == userId).Select(c => new Qiyas.BusinessLogicLayer.Entity.RoleSecurity.UserClaim(c) { context = db }).ToList();
                return items;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public ClaimsIdentity GetUserClaimsByUserId(int userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            var items = GetByUserId(userId);
            foreach (BusinessLogicLayer.Entity.RoleSecurity.UserClaim c in items)
            {
                Claim claim = new Claim(c.ClaimType, c.ClaimValue);
                claims.AddClaim(claim);
            }
            return claims;
        }

        public List<Qiyas.BusinessLogicLayer.Entity.RoleSecurity.UserClaim> GetAll()
        {
            try
            {
                return db.UserClaims.OrderBy(c => c.UserId).Select(c => new Qiyas.BusinessLogicLayer.Entity.RoleSecurity.UserClaim(c) { context = db }).ToList();
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public void DeleteByUserId(int userID)
        {
            var claims = (from x in db.UserClaims where x.UserId == userID select x);
            
            foreach(DataAccessLayer.UserClaim claim in claims)
            {
                db.UserClaims.DeleteOnSubmit(claim);
            }
            db.SubmitChanges();
        }
    }
}
