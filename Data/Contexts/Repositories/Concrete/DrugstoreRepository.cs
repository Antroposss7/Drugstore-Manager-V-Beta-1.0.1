using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Data.Contexts.Repositories.Abstract;

namespace Data.Contexts.Repositories.Concrete
{
    public class DrugstoreRepository : IDrugstoreRepository
    {
        static int id;
        public List<Drugstore> GetAll()
        {
            return DbContext.Drugstores;
        }
        public List<Drugstore> GetAllByOwner(int id)
        {
            return DbContext.Drugstores.Where(o => o.Owner.Id == id).ToList();
        }
        public List<Drug> Sale(int id)
        {
            return DbContext.Drugs.Where(s => s.Drugstore.Id == id).ToList();
        }
        public Drugstore Get(int id)
        {
            return DbContext.Drugstores.FirstOrDefault(s => s.Id == id);
        }
        public void Add(Drugstore drugStore)
        {
            id++;
            drugStore.Id = id;
            DbContext.Drugstores.Add(drugStore);
        }
        public void Update(Drugstore drugStore)
        {
            var dbDrugStore = DbContext.Drugstores.FirstOrDefault(s => s.Id == drugStore.Id);
            if (dbDrugStore is not null)
            {
                dbDrugStore.Name = drugStore.Name;
                dbDrugStore.ContactNumber = drugStore.ContactNumber;
                dbDrugStore.Email = drugStore.Email;
                dbDrugStore.Address = drugStore.Address;
                dbDrugStore.Owner = drugStore.Owner;
            }
        }
        public void Delete(Drugstore drugStore)
        {
            DbContext.Drugstores.Remove(drugStore);
        }

        public bool isDublicatedEmail(string email)
        {
            return DbContext.Drugstores.Any(s => s.Email == email);
        }
    }
}
    

