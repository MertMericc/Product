using Product.Entity.Dtos.PageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Entity.Dtos.ProductDtos
{
    public class ProductListFilterDto
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Search { get; set; }

        public PagingFilterDto PagingFilterDto { get; set; }

    }
}
