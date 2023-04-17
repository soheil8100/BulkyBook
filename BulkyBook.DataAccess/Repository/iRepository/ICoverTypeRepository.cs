﻿using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.iRepository
{
    public interface ICoverTypeRepository :iRepository<CoverType>
    {
        void Update(CoverType obj);
    }
}
