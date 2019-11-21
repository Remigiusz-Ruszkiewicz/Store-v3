using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Services
{
    public class ProductsService : IProductsService
    {
        private readonly DataContext dbContext;

        public ProductsService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> AddAsync(string name)
        {
            var product = new Product { Id = Guid.NewGuid(), Name = name };
            dbContext.Products.Add(product);

            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            dbContext.Products.Update(product);

            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return false;

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
