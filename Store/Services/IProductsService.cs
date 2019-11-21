using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface IProductsService 
    {
        Task<ICollection<Product>> GetAllAsync();

        Task<Product> GetAsync(Guid id);

        Task<Product> AddAsync(string name);

        Task<Product> UpdateAsync(Product product);

        Task<bool> DeleteAsync(Guid id);
    }
}
