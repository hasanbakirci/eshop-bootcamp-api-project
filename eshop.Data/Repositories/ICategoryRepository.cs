using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models.Entities;

namespace eshop.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> CategoryIsExist(int id);
    }
}