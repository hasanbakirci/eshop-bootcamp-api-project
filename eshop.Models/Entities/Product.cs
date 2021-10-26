using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Models.Entities
{
   public class Product :IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockCount { get; set; }
        public DateTime CreatedSate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
