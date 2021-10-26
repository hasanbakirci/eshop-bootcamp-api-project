using eshop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Data.Repositories
{
   public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByName(string name);
        //Task<int> AddProduct(Product product);
        Task<bool> ProductIsExist(int id);
    }
}
