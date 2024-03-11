using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> ,IProductRepository
    {
        private ApplicationDbContext _db;  
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(Product obj)
        {
            //_db.Products.Update(obj); 
            // One problem is that when we call the Save method, it will save all the changes of every fields in the database.
            // but we only want to update the fields that we have changed.
            
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Author = obj.Author;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.CategoryId = obj.CategoryId;
            }
        }
    }
}
