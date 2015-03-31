using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Qiyas.WebAdmin.Models;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: User
        public ActionResult Index()
        {
            //List<BusinessLogicLayer.Entity.PPM.ExamType> types = new BusinessLogicLayer.Components.PPM.ExamTypeLogic().GetAll();
            //if(types == null)
            //    types = new List<BusinessLogicLayer.Entity.PPM.ExamType>();
            return View();
        }

        #region User Grid Methods
        [ValidateInput(false)]
        public ActionResult UserGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.Persons.PersonLogic().GetAll();
            return PartialView("_UserGridViewPartial", model);
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult UserGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.Persons.Person item)
        {
            if(ModelState.IsValid)
            {
                string firstName = "";
                string middleName = "";
                string lastName = "";
                GetFullNameFromDisplayName(item.DisplayName, out firstName, out middleName, out lastName);
                var user = new BusinessLogicLayer.Entity.Persons.Person { UserName = item.UserName, Email = item.Email, IsActive = item.IsActive, DisplayName = item.DisplayName.Trim(), FirstName = firstName, MiddleName = middleName, LastName = lastName, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now };
                try
                {
                    // Insert here a code to insert the new item in your model
                    bool usernameExists = UserNameExists(item.UserName, 0);
                    bool emailExists = EmailExists(item.Email, 0);

                    if (!usernameExists && !emailExists)
                    {
                        var result = UserManager.Create<BusinessLogicLayer.Entity.Persons.Person, string>(user, item.Password);
                        if (result.Succeeded)
                        {


                        }
                        else
                        {
                            string msgError = "";

                            foreach (var error in result.Errors)
                            {
                                if (error.Contains("Passwords"))
                                    msgError = Resources.MainResource.PasswordCheckError + " ";
                                else
                                    msgError += error + " ";
                            }
                            ViewData["EditError"] = msgError;
                        }
                    }
                    else if(usernameExists)
                    {
                        ViewData["EditError"] = Resources.MainResource.UserNameExists;
                    }
                    else if(emailExists)
                    {
                        ViewData["EditError"] = Resources.MainResource.EmailExists;
                    }
                }
                catch (Exception e)
                {
                    if(e.Message.Contains("null") && e.Message.Contains("password"))
                        ViewData["EditError"] = Resources.MainResource.RequiredPassword;
                    else
                        ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                string msgError = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        msgError += error.ErrorMessage + " ";
                    }
                }
                ViewData["EditError"] = msgError;
            }
            var model = new BusinessLogicLayer.Components.Persons.PersonLogic().GetAll();
            return PartialView("_UserGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.Persons.Person item)
        {
            if (ModelState.IsValid)
            {
                string firstName = "";
                string middleName = "";
                string lastName = "";
                GetFullNameFromDisplayName(item.DisplayName, out firstName, out middleName, out lastName);
                var user = new BusinessLogicLayer.Entity.Persons.Person(item.BusinessEntityId);
                if (user != null)
                {
                    user.UserName = item.UserName;
                    user.Email = item.Email;
                    user.IsActive = item.IsActive;
                    user.DisplayName = item.DisplayName.Trim();
                    user.FirstName = firstName;
                    user.MiddleName = middleName;
                    user.LastName = lastName;
                    user.ModifiedDate = DateTime.Now;

                    try
                    {
                        // Insert here a code to insert the new item in your model
                        bool usernameExists = UserNameExists(item.UserName, user.BusinessEntityId);
                        bool emailExists = EmailExists(item.Email, user.BusinessEntityId);

                        if (!usernameExists && !emailExists)
                        {
                            bool result = user.Save();
                            if (!user.CurrentCredential.IsNew)
                                user.CurrentCredential.Save();

                            if (result)
                            {
                                if (!string.IsNullOrEmpty(item.Password))
                                {
                                    string code = UserManager.GeneratePasswordResetToken(user.Id);
                                    var passresult = UserManager.ResetPassword(user.Id, code, item.Password);
                                    if (passresult.Succeeded)
                                    {

                                    }
                                    else
                                    {
                                        string msgError = "";

                                        foreach (var error in passresult.Errors)
                                        {
                                            if (error.Contains("Passwords"))
                                                msgError = Resources.MainResource.PasswordCheckError + " ";
                                            else
                                                msgError += error + " ";
                                        }
                                        ViewData["EditError"] = msgError;
                                    }
                                }

                            }
                        }
                        else if (usernameExists)
                        {
                            ViewData["EditError"] = Resources.MainResource.UserNameExists;
                        }
                        else if (emailExists)
                        {
                            ViewData["EditError"] = Resources.MainResource.EmailExists;
                        }
                    }
                    catch (Exception e)
                    {
                        if (e.Message.Contains("null") && e.Message.Contains("password"))
                            ViewData["EditError"] = Resources.MainResource.RequiredPassword;
                        else
                            ViewData["EditError"] = e.Message;
                    }
                }
                else
                {
                    ViewData["EditError"] = Resources.MainResource.NoUserError;
                }
            }
            else
            {
                string msgError = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        msgError += error.ErrorMessage + " ";
                    }
                }
                ViewData["EditError"] = msgError;
            }
            
            var model = new BusinessLogicLayer.Components.Persons.PersonLogic().GetAll();
            return PartialView("_UserGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserGridViewPartialDelete(System.Int32 BusinessEntityId)
        {
            
            if (BusinessEntityId >= 0)
            {
                try
                {
                    if(!UserHasDependentData(BusinessEntityId))
                    {
                        BusinessLogicLayer.Entity.Persons.Person person = new BusinessLogicLayer.Entity.Persons.Person(BusinessEntityId);
                        if (person.HasObject)
                        {
                            person.CurrentCredential.Delete();
                            person.Delete();
                        }
                            
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.UserHasDependentData;
                    }
                    
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.Persons.PersonLogic().GetAll();
            return PartialView("_UserGridViewPartial", model);
        }
        #endregion

        #region Helpers
        
        private bool UserNameExists(string username, int id)
        {
            var  currentUser = new BusinessLogicLayer.Entity.Persons.Person(id);
            var checkUser = new BusinessLogicLayer.Components.Persons.PersonLogic().GetByUserName(username);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.BusinessEntityId != checkUser.BusinessEntityId)
                return true;

            return false;
        }

        private bool EmailExists(string email, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.Persons.Person(id);
            var checkUser = new BusinessLogicLayer.Components.Persons.PersonLogic().GetByEmail(email);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.BusinessEntityId != checkUser.BusinessEntityId)
                return true;

            return false;
        }
        private void GetFullNameFromDisplayName(string displayName, out string firstName, out string middleName, out string lastName)
        {
            string[] name = displayName.Trim().Split(' ');
            firstName = "";
            middleName = "";
            lastName = "";
            if (name != null)
            {
                if (name.Length > 2)
                {
                    firstName = name[0];
                    lastName = name[name.Length - 1];
                    for (int i = 1; i < name.Length - 1; i++)
                    {
                        middleName = name[i] + " ";
                    }
                    middleName = middleName.Trim();
                }
                else if (name.Length == 2)
                {
                    firstName = name[0];
                    lastName = name[name.Length - 1];
                }
                else
                {
                    firstName = name[0];
                }
            }
        }

        private bool UserHasDependentData(int UserId)
        {
            ///TODO: Implement Stored Procedure for checking dependencies and rap it here
            return false;
        }
        #endregion
    }
}