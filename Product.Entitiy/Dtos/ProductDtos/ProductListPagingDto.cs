using Product.Core.Entity;
using Product.Entity.Dtos.PageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Entity.Dtos.ProductDtos
{
    public class ProductListPagingDto:IDto
    {
        public List<ProductListDto> Data { get; set; }
        public PagingDto Page { get; set; }
    }
}
