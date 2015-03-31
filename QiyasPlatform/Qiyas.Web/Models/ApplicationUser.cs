using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qiyas.Web.Models
{
    public class ApplicationUser : IUser
    {
        public string Id
        {
            get { throw new NotImplementedException(); }
        }

        public string UserName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}