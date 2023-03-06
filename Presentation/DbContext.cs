using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Data.Contexts
{
    public static class DbContext // Тут распологаются наши листы 
    {
        static DbContext()
        {
            Owners = new List<Owner>();
            Drugstores = new List<Drugstore>();
            Druggists = new List<Druggist>();
            Drugs = new List<Drug>();
            Admins = new List<Admin>();
        }
        public static List<Owner> Owners { get; set; }
        public static List<Drugstore> Drugstores { get; set; }
        public static List<Druggist> Druggists { get; set; }
        public static List<Drug> Drugs { get; set; }
        public static List<Admin> Admins { get; set; }

    }
}
