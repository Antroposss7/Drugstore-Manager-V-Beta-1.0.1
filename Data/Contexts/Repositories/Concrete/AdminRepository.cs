using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts.Repositories.Abstract;
using Core.Entities;
using Core.Helpers;

namespace Data.Contexts.Repositories.Concrete
{
    public class AdminRepository : IAdminRepository
    {
        public Admin GetByUsernameAndPassword(string username, string password)
        {
            return DbContext.Admins.FirstOrDefault(a => a.Username.ToLower() == username.ToLower() && PasswordHasher.Decrypt(a.Password) == password);
        }
    }
}
