using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.DataTransferObjects.Requests
{
    public class AddCategoryRequest
    {
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string Name { get; set; }
    }
}