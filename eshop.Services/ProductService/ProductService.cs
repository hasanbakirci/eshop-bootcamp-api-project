using eshop.Data.Repositories;
using eshop.Models.DataTransferObjects.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eshop.Services.Extensions;
using eshop.Models.Entities;
using AutoMapper;
using eshop.Models.DataTransferObjects.Requests;

namespace eshop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductSimpleResponse>> GetProducts()
        {
            var products = await _productRepository.GetAllEntities();
            var productSimpleResponses = products.ConvertToSimpleResponseListDto(_mapper);
            return productSimpleResponses;
        }

        public async Task<ProductDetailedResponse> GetProduct(int id)
        {
            Product product = await _productRepository.GetEntityById(id);
            var dto = product.ConvertToDetailedProductResponse(_mapper);
            dto.ImageUrls = new List<string>{
                "img1.jpg","img2.png"
            };
            return dto;
        }

        public async Task<int> AddProduct(AddProductRequest addProductRequest)
        {
            var product = addProductRequest.ConvertToProduct(_mapper);
            int id = await _productRepository.Add(product);
            return id;
        }

        public async Task<IEnumerable<ProductSimpleResponse>> GetProductsByName(string name)
        {
            var products = await _productRepository.GetProductsByName(name);
            var response = products.ConvertToSimpleResponseListDto(_mapper);
            return response;
        }

        public async Task<bool> ProductIsExist(int id)
        {
            return await _productRepository.ProductIsExist(id);
        }

        public async Task<ProductSimpleResponse> UpdateProduct(int id,UpdateProductRequest updateProductRequest)
        {
            var product = updateProductRequest.ConvertToProduct(_mapper);
            var result = await _productRepository.Update(id,product);
            return result.ConvertToSimpleProductDto(_mapper);
        }

        public async Task<int> DeleteProduct(int id)
        {
            return await _productRepository.Delete(id);
        }
    }
}
