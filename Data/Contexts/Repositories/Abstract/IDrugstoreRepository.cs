using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts.Repositories.Abstract
{
    public interface IDrugstoreRepository : IRepository<Drugstore>
    {
        bool isDublicatedEmail(string email);
        List<Drugstore> GetAllByOwner(int id);
        List<Drug> Sale(int id);
    }
}



