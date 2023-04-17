using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.iRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : iRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
                _db = dbContext;
            this.dbSet = dbContext.Set<T>();
            //_db.Products.Include(u=>u.Category).Include(u=>u.CoverType)
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        //InclodeProp Category,CoverType
        public IEnumerable<T> GetAll(string? incldeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (incldeProperties != null)
            {
                foreach(var includeProp in incldeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                     query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? incldeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (incldeProperties != null)
            {
                foreach (var includeProp in incldeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);

        }
    }
}
