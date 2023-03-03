using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Data.Contexts.Repositories.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T value);
        T Get(int id);
        List<T> GetAll();

        void Update(T value);
        void Delete(T value);
    }
}
