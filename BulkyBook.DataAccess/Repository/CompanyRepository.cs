﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    internal class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {   
            _db = db;
        }

        public void Update(Company obj)
        {
        
            _db.Companies.Update(obj);
        }
    }
}
