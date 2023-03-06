using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Entities;
using Data.Contexts.Repositories.Abstract;

namespace Data.Contexts.Repositories.Concrete
{
    public class OwnerRepository : IOwnerRepository
    {
        static int id;

        public List<Owner> GetAll()
        {
            return DbContext.Owners;
        }
       
        public Owner Get(int id)
        {
            return DbContext.Owners.FirstOrDefault(o => o.Id == id);
        }
        public void Update(Owner owner)
        {
            var dbOwner = DbContext.Owners.FirstOrDefault(o => o.Id == owner.Id);
            if (dbOwner != null)
            {
                dbOwner.Name = owner.Name;
                dbOwner.Surname = owner.Surname;
            }

        }
        public void Add(Owner owner)
        {
            id++;
            owner.Id = id;
            DbContext.Owners.Add(owner);
        }

        public void Delete(Owner owners)
        {
            DbContext.Owners.Remove(owners);
        }



    }
}
