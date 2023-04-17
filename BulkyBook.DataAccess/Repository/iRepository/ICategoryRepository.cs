﻿using BulkyBook.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.iRepository
{
    public interface ICategoryRepository :iRepository<Category>
    {
        void Update(Category obj);
    }
}
