﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts.Repositories.Abstract
{
    public interface IDrugRepository : IRepository<Drug>
    {
        List<Drug> GetAllByDrugStore(int id);
    }
}
