using eshop.Models;
using eshop.Models.DataTransferObjects.Requests;
using eshop.Models.DataTransferObjects.Responses;
using eshop.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async  Task<IActionResult> GetProducts()
        {            
            var products = await _productService.GetProducts();
            return Ok(products);
        }
        [HttpGet("Search/id/{id}")]
        //[Authorize]
        public async Task<IActionResult> GetProductId(int id){
            ProductDetailedResponse product = await _productService.GetProduct(id);
            return Ok(product);
        }
        [HttpGet("Search/name/{name}")]
        //[Authorize]
        public async Task<IActionResult> GetProductName(string name){
            var products = await _productService.GetProductsByName(name);
            return Ok(products);
        }
        [HttpPost]
        //[Authorize(Roles ="Admin,Editor")]
        public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest){
            if(ModelState.IsValid){
                int lastProductId = await _productService.AddProduct(addProductRequest);
                return Ok(new {id = lastProductId, name = addProductRequest.Name});
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest updateProductRequest){
            var isExist = await _productService.ProductIsExist(id);
            if(isExist){
                if(ModelState.IsValid){
                    var product = await _productService.UpdateProduct(id,updateProductRequest);
                    return Ok(product);
                }
                return BadRequest(ModelState);
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var isExist = await _productService.ProductIsExist(id);
            if(isExist){
                var product = await _productService.DeleteProduct(id);
                return Ok(product);
            }
            return BadRequest(ModelState);
        }
    }
}
