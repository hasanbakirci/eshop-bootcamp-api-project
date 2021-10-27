using eshop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Data.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        private List<Product> _products;
        public FakeProductRepository()
        {
            _products = new List<Product>
            {
                new Product {Id=1,Name="Name 1",Price=100,Description="Description 1",ImageUrl="url 1"},
                new Product {Id=2,Name="Name 2",Price=200,Description="Description 2",ImageUrl="url 2"},
                new Product {Id=3,Name="Name 3",Price=300,Description="Description 3",ImageUrl="url 3"},
                new Product {Id=4,Name="Name 4",Price=400,Description="Description 4",ImageUrl="url 4"},
                new Product {Id=5,Name="Name 5",Price=500,Description="Description 5",ImageUrl="url 5"},
                new Product {Id=6,Name="Name 6",Price=600,Description="Description 6",ImageUrl="url 6"},
                new Product {Id=7,Name="Name 7",Price=700,Description="Description 7",ImageUrl="url 7"}

            };
        }

        public async Task<int> Add(Product product)
        {
            product.Id = _products[_products.Count -1].Id +1;
            _products.Add(product);
            return await Task.FromResult(product.Id);
        }

        public Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllEntities()
        {
            return await Task.FromResult(_products);
        }

        public async Task<Product> GetEntityById(int id)
        {
            return await Task.FromResult(_products.Find(x => x.Id == id));
        }

        public Task<bool> ProductIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(int id,Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
