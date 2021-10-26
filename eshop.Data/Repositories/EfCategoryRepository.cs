using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Data.Context;
using eshop.Models.Entities;

namespace eshop.Data.Repositories
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly EshopDbContext _context;

        public EfCategoryRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Category entity)
        {
            _context.categories.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllEntities()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}