using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Drug : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }  
        public int Count { get; set; }
        public Drugstore Drugstore { get; set; }
    }
}
