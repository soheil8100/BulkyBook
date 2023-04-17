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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
           // _db.Products.Update(obj);
           var objFram = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFram != null)
            {
                objFram.Title = obj.Title;
                objFram.ISBN = obj.ISBN;
                objFram.Description = obj.Description;
                objFram.Price = obj.Price;
                objFram.Price50 = obj.Price50;
                objFram.Price100 = obj.Price100;
                objFram.ListPrice = obj.ListPrice;
                objFram.CategoryId = obj.CategoryId;
                objFram.Author = obj.Author;
                objFram.CoverTypeID = obj.CoverTypeID;
                if (obj.ImageUrl != null)
                {
                    objFram.ImageUrl = obj.ImageUrl;
                }




            }
        }
    }
}
