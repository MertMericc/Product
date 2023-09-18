using Product.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Entity.Dtos.ProductDtos
{
    public class ProductAddDto:IDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
