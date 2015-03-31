using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Qiyas.BusinessLogicLayer.Entity.Persons;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Qiyas.WebAdmin.Models;
using Qiyas.WebAdmin.Common.Security;
namespace Qiyas.WebAdmin
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Common.Email.EmailManager.SendEmailAsync(message);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            
            // Plug in your SMS service here to send a text message.
            
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<Person>
    {
        public ApplicationUserManager(IUserStore<Person> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            
            var manager = new ApplicationUserManager(new CustomUserStore<Person>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Person>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<BusinessLogicLayer.Entity.Persons.Person>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<BusinessLogicLayer.Entity.Persons.Person>
            {
                
                Subject = "Security Code",
                BodyFormat = "{0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<BusinessLogicLayer.Entity.Persons.Person>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /// <summary>
        /// Overrided NotifyTwoFactorTokenAsync for Customizing Email Format before sending it
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="twoFactorProvider"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public override Task<IdentityResult> NotifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
        {
            string tmpl = token;
            #region Loading Email Template
            string tmplName = "TwoWayAuthentication.html";
            if (twoFactorProvider == "Email Code")
            {
                string title;
                title = Resources.MainResource.TwoWayAuthTitle;


                try
                {
                    tmpl = Common.Email.EmailManager.getEmailTemplate(tmplName);
                }
                catch
                {
                    // error loading template file
                    throw new Exception(string.Format(Resources.MainResource.EmailTemplateError, tmplName));
                }

                BusinessLogicLayer.Entity.Persons.Person person = new Person(Convert.ToInt32(userId));
                if (person != null)
                {
                    tmpl = tmpl.Replace("##USERNAME##", person.DisplayName);
                    tmpl = tmpl.Replace("##AUTHCODE##", token);
                }
            }
            
            #endregion

            return base.NotifyTwoFactorTokenAsync(userId, twoFactorProvider, tmpl);
        }

    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<BusinessLogicLayer.Entity.Persons.Person, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(BusinessLogicLayer.Entity.Persons.Person user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            string user = userName;
            BusinessLogicLayer.Components.Persons.PersonLogic personLogic = new BusinessLogicLayer.Components.Persons.PersonLogic();
            BusinessLogicLayer.Entity.Persons.Person person = personLogic.GetByUserName(userName);
            if (person == null)
                person = personLogic.GetByEmail(userName);
            if (person != null)
                user = person.UserName;
            if (person != null)
                if (!person.IsActive)
                    return Task.FromResult(SignInStatus.LockedOut);
            return base.PasswordSignInAsync(user, password, isPersistent, shouldLockout);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        
    }
}
