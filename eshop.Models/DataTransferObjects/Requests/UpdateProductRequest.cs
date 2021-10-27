using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.DataTransferObjects.Requests
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockCount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}