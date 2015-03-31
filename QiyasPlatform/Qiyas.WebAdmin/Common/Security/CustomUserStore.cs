using Microsoft.AspNet.Identity;
using Qiyas.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Security.Claims;
namespace Qiyas.WebAdmin.Common.Security
{
    public class CustomUserStore<TUser> : IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserEmailStore<TUser>,
        IUserPhoneNumberStore<TUser>,
        IUserTwoFactorStore<TUser, string>,
        IUserLockoutStore<TUser, string>,
        IUserStore<TUser> where TUser : BusinessLogicLayer.Entity.Persons.Person
    {

        #region Properties
        private BusinessLogicLayer.Components.Persons.PersonLogic personLogic = new BusinessLogicLayer.Components.Persons.PersonLogic();
        private BusinessLogicLayer.Components.RoleSecurity.UserClaimLogic userClaimLogic = new BusinessLogicLayer.Components.RoleSecurity.UserClaimLogic();
        private BusinessLogicLayer.Components.RoleSecurity.UserLoginProviderLogic userLoginProviderLogic = new BusinessLogicLayer.Components.RoleSecurity.UserLoginProviderLogic();
        private BusinessLogicLayer.Components.RoleSecurity.PersonRoleLogic personRoleLogic = new BusinessLogicLayer.Components.RoleSecurity.PersonRoleLogic();
        private BusinessLogicLayer.Components.RoleSecurity.RoleLogic roleLogic = new BusinessLogicLayer.Components.RoleSecurity.RoleLogic();
        //private RoleTable roleTable;
        //private UserRolesTable userRolesTable;
        //private UserClaimsTable userClaimsTable;
        //private UserLoginsTable userLoginsTable;
        //public MySQLDatabase Database { get; private set; }
        #endregion

        #region User Store
        public System.Threading.Tasks.Task CreateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if(user.CurrentCredential == null)
                throw new ArgumentNullException("user.Credential");
            BusinessLogicLayer.Entity.Persons.BusinessEntity be = new BusinessLogicLayer.Entity.Persons.BusinessEntity();
            be.RowGuid = Guid.NewGuid();
            be.Save();
            user.BusinessEntityId = be.BusinessEntityId;

            user.Save();
            BusinessLogicLayer.Entity.Persons.Credential cred = user.CurrentCredential;
            cred.BusinessEntityId = user.BusinessEntityId;
            if (string.IsNullOrEmpty(cred.Username))
                cred.Username = cred.Email;
            if (string.IsNullOrEmpty(cred.Email))
                cred.Email = cred.Username;
            cred.IsNew = true;
            cred.IsActive = true;
            cred.IsActivated = true;
            cred.Save();
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task DeleteAsync(TUser user)
        {
            
            if (user == null)
                throw new ArgumentNullException("user");
            if(user.CurrentCredential == null)
                throw new ArgumentNullException("user.Credential");
            user.CurrentCredential.Delete();
            user.CurrentCredential = null;
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task<TUser> FindByIdAsync(int userId)
        {

            if (userId == 0)
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            TUser result = personLogic.GetByID(userId) as TUser;
            if (result != null)
            {
                return Task.FromResult<TUser>(result);
            }

            return Task.FromResult<TUser>(null);
        }

        public System.Threading.Tasks.Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            List<TUser> result = personLogic.GetByUserNameList(userName) as List<TUser>;

            // Should I throw if > 1 user?
            if (result != null && result.Count == 1)
            {
                return Task.FromResult<TUser>(result[0]);
            }

            return Task.FromResult<TUser>(null);
        }

        public System.Threading.Tasks.Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Save();
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            personLogic = null;
            userClaimLogic = null;
            userLoginProviderLogic = null;
            personRoleLogic = null;
            roleLogic = null;
        }
        #endregion

        #region User Claim Store
        public Task AddClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("user");
            }

            BusinessLogicLayer.Entity.RoleSecurity.UserClaim claimEntity = new BusinessLogicLayer.Entity.RoleSecurity.UserClaim();
            claimEntity.UserId = user.BusinessEntityId;
            claimEntity.ClaimType = claim.Type;
            claimEntity.ClaimValue = claim.Value;
            claimEntity.Save();

            return Task.FromResult<object>(null);
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(TUser user)
        {
            ClaimsIdentity identity = userClaimLogic.GetUserClaimsByUserId(user.BusinessEntityId);

            return Task.FromResult<IList<Claim>>(identity.Claims.ToList());
        }

        public Task RemoveClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            userClaimLogic.DeleteByUserId(user.BusinessEntityId);
            return Task.FromResult<object>(null);
        }
        #endregion
        
        #region User Login Store
        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            BusinessLogicLayer.Entity.RoleSecurity.UserLoginProvider loginEntity = new BusinessLogicLayer.Entity.RoleSecurity.UserLoginProvider();
            loginEntity.UserId = user.BusinessEntityId;
            loginEntity.LoginProvider = login.LoginProvider;
            loginEntity.ProviderKey = login.ProviderKey;
            loginEntity.Save();

            return Task.FromResult<object>(null);
        }

        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userId = userLoginProviderLogic.GetUserIdByLogin(login);
            if (userId != null)
            {
                TUser user = new BusinessLogicLayer.Entity.Persons.Person(Convert.ToInt32(userId)) as TUser;
                if (user != null)
                {
                    return Task.FromResult<TUser>(user);
                }
            }

            return Task.FromResult<TUser>(null);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            List<UserLoginInfo> userLogins = new List<UserLoginInfo>();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            List<UserLoginInfo> logins = userLoginProviderLogic.GetUserLoginsInfoByUserId(user.BusinessEntityId);
            if (logins != null)
            {
                return Task.FromResult<IList<UserLoginInfo>>(logins);
            }

            return Task.FromResult<IList<UserLoginInfo>>(null);
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            userLoginProviderLogic.DeleteByUserId(user.BusinessEntityId);

            return Task.FromResult<Object>(null);
        }
        #endregion

        #region User Role Store
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }
            BusinessLogicLayer.Entity.RoleSecurity.Role role = new BusinessLogicLayer.Entity.RoleSecurity.Role(roleName);
            
            if (role != null)
            {
                BusinessLogicLayer.Entity.RoleSecurity.PersonRole prole = new BusinessLogicLayer.Entity.RoleSecurity.PersonRole();
                prole.PersonId = user.BusinessEntityId;
                prole.RoleId = role.RoleId;
                prole.Save();
            }

            return Task.FromResult<object>(null);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            List<string> roles = personRoleLogic.GetByUserIdStringList(user.BusinessEntityId);
            {
                if (roles != null)
                {
                    return Task.FromResult<IList<string>>(roles);
                }
            }

            return Task.FromResult<IList<string>>(null);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            List<string> roles = personRoleLogic.GetByUserIdStringList(user.BusinessEntityId);
            {
                if (roles != null && roles.Contains(roleName))
                {
                    return Task.FromResult<bool>(true);
                }
            }

            return Task.FromResult<bool>(false);
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }
            BusinessLogicLayer.Entity.RoleSecurity.Role role = new BusinessLogicLayer.Entity.RoleSecurity.Role(roleName);
            if(role != null)
            {
                BusinessLogicLayer.Entity.RoleSecurity.PersonRole prole = new BusinessLogicLayer.Entity.RoleSecurity.PersonRole(role.RoleId, user.BusinessEntityId);
                if (prole != null)
                    prole.Delete();
            }
            

            return Task.FromResult<Object>(null);
        }


        public Task<TUser> FindByIdAsync(string userIdStr)
        {
            if(string.IsNullOrEmpty(userIdStr))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }
            int userId = 0;
            Int32.TryParse(userIdStr, out userId);
            if (userId == 0)
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            TUser result = personLogic.GetByID(userId) as TUser;
            if (result != null)
            {
                return Task.FromResult<TUser>(result);
            }

            return Task.FromResult<TUser>(null);
        }
        #endregion

        #region User Password Store
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            string passwordHash = null;
            if (user.CurrentCredential != null)
                if (!string.IsNullOrEmpty(user.CurrentCredential.PasswordHash))
                    passwordHash = user.CurrentCredential.PasswordHash;

            return Task.FromResult<string>(passwordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            bool hasPassword = false;
            if (user.CurrentCredential != null)
                if (!string.IsNullOrEmpty(user.CurrentCredential.PasswordHash))
                    hasPassword = true;

            return Task.FromResult<bool>(hasPassword);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.PasswordHash = passwordHash;
            user.CurrentCredential.Save();
            return Task.FromResult<Object>(null);
        }
        #endregion

        #region User Security Stamp Store
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.CurrentCredential.PasswordSalt);
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if(user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.PasswordSalt = stamp;
            user.CurrentCredential.Save();

            return Task.FromResult(0);
        }
        #endregion

        #region Queryable User Store
        public IQueryable<TUser> Users
        {
            get { return personLogic.GetAll().AsQueryable<BusinessLogicLayer.Entity.Persons.Person>() as IQueryable<TUser>; }
        }
        #endregion

        #region User Email Store
        public Task<TUser> FindByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }

            TUser result = personLogic.GetByEmail(email) as TUser;
            if (result != null)
            {
                return Task.FromResult<TUser>(result);
            }

            return Task.FromResult<TUser>(null);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            return Task.FromResult(user.CurrentCredential.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            if (!user.CurrentCredential.EmailConfirmed.HasValue)
                return Task.FromResult(false);
            return Task.FromResult(user.CurrentCredential.EmailConfirmed.Value);
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.Email = email;
            user.CurrentCredential.Save();
            

            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.EmailConfirmed = confirmed;
            user.CurrentCredential.Save();


            return Task.FromResult(0);
        }
        #endregion

        #region User Phone Number Store
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            return Task.FromResult(user.CurrentCredential.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            if (!user.CurrentCredential.PhoneNumberConfirmed.HasValue)
                return Task.FromResult(false);
            return Task.FromResult(user.CurrentCredential.PhoneNumberConfirmed.Value);
        }

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.PhoneNumber = phoneNumber;
            user.CurrentCredential.Save();


            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.PhoneNumberConfirmed = confirmed;
            user.CurrentCredential.Save();


            return Task.FromResult(0);
        }
        #endregion

        #region User Two Factor Store
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            if (!user.CurrentCredential.TwoFactorEnabled.HasValue)
                return Task.FromResult(false);
            return Task.FromResult(user.CurrentCredential.TwoFactorEnabled.Value);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.TwoFactorEnabled = enabled;
            user.CurrentCredential.Save();


            return Task.FromResult(0);
        }
        #endregion

        #region User Lockout Store

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            if (!user.CurrentCredential.AccessFailedCount.HasValue)
                user.CurrentCredential.AccessFailedCount = 0;

            return Task.FromResult(user.CurrentCredential.AccessFailedCount.Value);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            if (!user.CurrentCredential.LockoutEnabled.HasValue)
                user.CurrentCredential.LockoutEnabled = false;
            return Task.FromResult(user.CurrentCredential.LockoutEnabled.Value);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            return
                Task.FromResult(user.CurrentCredential.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.CurrentCredential.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            if (!user.CurrentCredential.AccessFailedCount.HasValue)
                user.CurrentCredential.AccessFailedCount = 0;
            user.CurrentCredential.AccessFailedCount++;
            user.CurrentCredential.Save();


            return Task.FromResult(user.CurrentCredential.AccessFailedCount.Value);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }
            user.CurrentCredential.AccessFailedCount = 0;
            user.CurrentCredential.Save();


            return Task.FromResult(user.CurrentCredential.AccessFailedCount.Value);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.LockoutEnabled = enabled;
            user.CurrentCredential.Save();


            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.CurrentCredential == null)
            {
                throw new ArgumentNullException("user.Credential");
            }

            user.CurrentCredential.LockoutEndDateUtc = lockoutEnd.UtcDateTime;
            user.CurrentCredential.Save();


            return Task.FromResult(0);
        }
        #endregion

        
    }
}