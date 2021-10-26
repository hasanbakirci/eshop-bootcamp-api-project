using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models.DataTransferObjects.Requests;

namespace eshop.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<int> AddNewCategory(AddCategoryRequest request);
    }
}