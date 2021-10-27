using eshop.Models.DataTransferObjects.Requests;
using eshop.Models.DataTransferObjects.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductSimpleResponse>> GetProducts();
        Task<ProductDetailedResponse> GetProduct(int id);
        Task<int> AddProduct(AddProductRequest addProductRequest);
        Task<IEnumerable<ProductSimpleResponse>> GetProductsByName(string name);
        Task<bool> ProductIsExist(int id);
        Task<ProductSimpleResponse> UpdateProduct(int id,UpdateProductRequest updateProductRequest);
        Task<int> DeleteProduct(int id);
    }
}
