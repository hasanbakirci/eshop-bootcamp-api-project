using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eshop.API.Filters
{
    public class ItemExistFilter : IAsyncActionFilter
    {
        private readonly IProductService _productService;

        public ItemExistFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // bu filtrenin uygulandıgı action'ın id parametresi olmalı !
            if(!(context.ActionArguments["id"] is int id)){
                context.Result = new BadRequestResult();
                return;
            }
            var isExist = await _productService.ProductIsExist(id);
            if(!isExist){
                context.Result = new NotFoundObjectResult($"{id} id'ye ait ürün bulunamadı");
                return;
            }
            await next();
        }
    }
}