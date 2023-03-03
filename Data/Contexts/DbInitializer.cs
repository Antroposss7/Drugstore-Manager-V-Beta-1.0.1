using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;

namespace Data.Contexts
{
    public class DbInitializer
    {
        private static int id;

        public static void SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Id = ++id,
                    Username = "admin1",
                    Password = PasswordHasher.Encrypt("123")
                },
                new Admin
                {
                    Id = ++id,
                    Username = "admin2",
                    Password = PasswordHasher.Encrypt("1234")

                },
                new Admin
                {
                    Id = ++id,
                    Username = "admin3",
                    Password = PasswordHasher.Encrypt("12345")
                }
            };
            DbContext.Admins.AddRange(admins);
        }
    }
}