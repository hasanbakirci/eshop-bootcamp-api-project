using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Data.Context;
using eshop.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> CategoryIsExist(int id)
        {
            return await _context.categories.AnyAsync(c => c.Id == id);
        }

        public async Task<int> Delete(int id)
        {
            var category = await _context.categories.SingleOrDefaultAsync(c => c.Id == id);
            _context.Remove(category);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Category>> GetAllEntities()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task<Category> GetEntityById(int id)
        {
            return await _context.categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> Update(int id,Category entity)
        {
            _context.categories.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}