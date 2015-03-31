using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qiyas.WebAdmin.Common.Email
{
    [Serializable]
    public class EmailManagerException : Exception
    {
        public EmailManagerException() : base("Email Error") { }
    }
}