using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.iRepository
{
    public interface iRepository<T> where T : class
    {
        //T - Category
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? incldeProperties = null);
        IEnumerable<T> GetAll(string? incldeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
}
