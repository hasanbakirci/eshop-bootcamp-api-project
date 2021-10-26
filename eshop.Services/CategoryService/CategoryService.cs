using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eshop.Data.Repositories;
using eshop.Models.DataTransferObjects.Requests;
using eshop.Services.Extensions;

namespace eshop.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<int> AddNewCategory(AddCategoryRequest request)
        {
            var category = request.ConvertToCategory(_mapper);
            int id = await _categoryRepository.Add(category);
            return id;
        }
    }
}