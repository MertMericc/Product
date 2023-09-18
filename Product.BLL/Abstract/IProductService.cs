using Product.Core.Result;
using Product.Entity.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.BLL.Abstract
{
    public interface IProductService
    {
        IDataResult<bool> AddProduct(ProductAddDto productAddDto);
        IDataResult<bool> UpdateProduct(ProductUpdateDto productUpdateDto);
        IDataResult<bool> ChangeStatusProduct(int productId);
        IDataResult<List<ProductListDto>> GetAllProduct();
        IDataResult<ProductListDto> GetProductById(int productId);
        IDataResult<ProductListPagingDto> GetListProductWithPage(ProductListFilterDto productListFilterDto);
        
    }
}
