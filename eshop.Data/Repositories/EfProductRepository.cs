using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Data.Context;
using eshop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshop.Data.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly EshopDbContext _context;

        public EfProductRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Product entity)
        {
            _context.products.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> Delete(int id)
        {
            // singleordefault = birden fazla element dönüyosa hata verir
            // firstordefault = birden fazla element dönerse hata vermez
            var product = await _context.products.SingleOrDefaultAsync(p => p.Id == id);
            _context.products.Remove(product);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllEntities()
        {
            return await _context.products.ToListAsync();
            //return await _context.products.Skip(0).Take(5).ToListAsync();
            // Skip((page-1)*5) böylece ihtiyaç kadarını alır
            //1. sayfa  0 satır atla 5 satır al
            //2. sayfa 5 satır atla 5 satır al 
            //3. sayfa 10 satır atla 5 satır al 
        }

        public async Task<Product> GetEntityById(int id)
        {
            return await _context.products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _context.products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<bool> ProductIsExist(int id)
        {
            return await _context.products.AnyAsync(p => p.Id == id);
        }

        public async Task<Product> Update(Product entity)
        {
            _context.products.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}