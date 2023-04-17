using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _db = dbContext;    
        }
        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
