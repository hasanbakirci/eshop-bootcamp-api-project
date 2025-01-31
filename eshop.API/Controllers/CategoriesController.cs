using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models.DataTransferObjects.Requests;
using eshop.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace eshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories(){
            var result = await _categoryService.GetCategories();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequest request){
            if(ModelState.IsValid){
                int lastCategoryId = await _categoryService.AddCategory(request);
                return Ok(new{id = lastCategoryId, name = request.Name});
            }
            return BadRequest(ModelState);
        }
    }
}