using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qiyas.Web.Models
{
    public class CustomUserSore<T> : IUserStore<T> where T : ApplicationUser
    {

        public System.Threading.Tasks.Task CreateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<T> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<T> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}