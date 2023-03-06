using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{

    public class Drug : BaseEntity
    {
       
        public Drug()
        { 
          
          Drugstore = new Drugstore();
        }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public Drugstore Drugstore { get; set; }
        public string Description { get; set; }

        
    }
}
