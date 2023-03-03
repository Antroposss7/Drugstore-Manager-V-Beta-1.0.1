using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts.Repositories.Abstract
{
    public interface IAdminRepository
    {
        Admin GetByUsernameAndPassword(string username, string password);
    }
}
