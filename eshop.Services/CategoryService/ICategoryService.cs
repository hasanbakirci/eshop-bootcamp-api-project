using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models.DataTransferObjects.Requests;
using eshop.Models.DataTransferObjects.Responses;

namespace eshop.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategorySimpleResponse>> GetCategories();
        Task<CategorySimpleResponse> GetCategory(int id);
        Task<int> AddCategory(AddCategoryRequest request);
    }
}