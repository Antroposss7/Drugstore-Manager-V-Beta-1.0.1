using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Data.Contexts.Repositories.Abstract;

namespace Data.Contexts.Repositories.Concrete
{
    public class DruggistRepository : IDruggistRepository
    {
        static int id;
        public void Add(Druggist druggist)
        {
            id++;
            druggist.Id = id;
            DbContext.Druggists.Add(druggist);
        }

        public Druggist Get(int id)
        {
            return DbContext.Druggists.FirstOrDefault(d => d.Id == id);
        }

        public List<Druggist> GetAll()
        {
            return DbContext.Druggists;
        }

        public void Update(Druggist druggist)
        {
            var dbDruggist = DbContext.Druggists.FirstOrDefault(d => d.Id == druggist.Id);
            if (dbDruggist is not null)
            {
                dbDruggist.Name = druggist.Name;
                dbDruggist.Surname = druggist.Surname;
                dbDruggist.Age = druggist.Age;
                dbDruggist.Experience = druggist.Experience;
            }
        }

        public void Delete(Druggist druggist)
        {
            DbContext.Druggists.Remove(druggist); 
        }

        public List<Druggist> GetAllByDrugStore(int id)
        {
            return DbContext.Druggists.Where(d => d.Drugstore.Id == id).ToList();
        }
    }
}
