using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eshop.Models.DataTransferObjects.Requests;
using eshop.Models.DataTransferObjects.Responses;
using eshop.Models.Entities;

namespace eshop.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductSimpleResponse>(); //.ReverseMap(); tersi çevirme ihtiyacı olursa kullanılabilir.
            CreateMap<Product,ProductDetailedResponse>();
            CreateMap<AddProductRequest,Product>();


            CreateMap<AddCategoryRequest,Category>();
        }
    }
}