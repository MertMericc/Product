using Product.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Entity.Dtos.ProductDtos
{
    public class ProductUpdateDto:IDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }

    }
}
