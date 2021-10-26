using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshop.Data.Context
{
    public class EshopDbContext : DbContext
    {
        //
        // Hangi provider ve hangi isimle bu Db oluşacak ctor startupdan istiyor.
        public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
        {
        }
        //
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(p=>p.Name)
            .IsRequired() // boş geçilemez
            .HasMaxLength(150); // uzunlugu max 150 olur

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category) // bir ürünün bir categorisi var
            .WithMany(category => category.Products) // kategorinin birden fazla ürünü olabilir
            .HasForeignKey(p => p.CategoryId) //forainkeyi gösteriyoruz
            .OnDelete(DeleteBehavior.NoAction); // ürün silindiğinde ne olsun / silmesine izin verme update et
        }

        // database oluşturulurken kullanılan yöntem bu
        // bu uygulamayı ayağa kaldırırken başka bir db ye geçmek isterse startupda olmalı burada olmamalı
        // yani optionBuilder i startupdan alıcaz ctor ile
        // yukarda ctor tanımlandı
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        
        }
    }
}