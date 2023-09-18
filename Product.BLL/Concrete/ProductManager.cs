using Product.BLL.Abstract;
using Product.BLL.Constants;
using Product.Core.Result;
using Product.Dal.Abstract;
using Product.Entity.Concrete;
using Product.Entity.Dtos.PageDtos;
using Product.Entity.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.BLL.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<bool> AddProduct(ProductAddDto productAddDto)
        {
            try
            {
                if (productAddDto == null)
                {
                    return new ErrorDataResult<bool>(false, "data is null", Messages.err_null);
                }
                var addedProduct = new Products
                {
                    Name = productAddDto.Name,
                    Code = productAddDto.Code,
                    Price = productAddDto.Price,
                    CreatedDate = DateTime.Now,
                    ImageUrl = productAddDto.ImageUrl,
                    Status = true
                };
                _productDal.Add(addedProduct);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<bool> ChangeStatusProduct(int productId)
        {
            try
            {
                var getProduct = _productDal.Get(x => x.Id == productId);
                if (getProduct == null)
                {
                    return new ErrorDataResult<bool>(false, "product not found", Messages.err_null);
                }
                getProduct.Status = !getProduct.Status;
                _productDal.Update(getProduct);
                return new SuccessDataResult<bool>(true, "product status updated successfully", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<List<ProductListDto>> GetAllProduct()
        {
            try
            {
                var getProduct = _productDal.GetList(x => x.Status == true).ToList();
                if (getProduct.Count <= 0)
                {
                    return new ErrorDataResult<List<ProductListDto>>(new List<ProductListDto>(), "products not found", Messages.err_null);
                }
                var list = new List<ProductListDto>();
                foreach (var product in getProduct)
                {
                    list.Add(new ProductListDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Code = product.Code,
                        Price = product.Price,
                        CreatedDate = product.CreatedDate,
                        ImageUrl = product.ImageUrl,
                        Status = product.Status
                    });
                }
                return new SuccessDataResult<List<ProductListDto>>(list, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductListDto>>(new List<ProductListDto>(), ex.Message, Messages.err_null);
            }
        }

        public IDataResult<ProductListPagingDto> GetListProductWithPage(ProductListFilterDto productListFilterDto)
        {
            try
            {
                var products = _productDal.GetList().ToList();
                if (products != null)
                {
                    var productListDto = new List<ProductListDto>();
                    foreach (var product in products)
                    {
                        productListDto.Add(new ProductListDto
                        {
                            Id = product.Id,
                            Status = product.Status,
                            Code = product.Code,
                            CreatedDate = product.CreatedDate,
                            ImageUrl = product.ImageUrl,
                            Name = product.Name,
                            Price = product.Price,
                        });
                    }
                    if (productListFilterDto.Search != null)
                    {
                        var searching = productListFilterDto.Search;
                        productListDto = productListDto.Where(x => x.ImageUrl.Trim().ToLower().Contains(searching.Trim().ToLower()) || x.Name.Trim().ToLower().Contains(searching.Trim().ToLower())).ToList();
                    }
                    else if (productListFilterDto.Name != null)
                    {
                        productListDto = productListDto.Where(x => x.Name.Trim().ToLower() == productListFilterDto.Name.Trim().ToLower()).ToList();
                    }
                    else if (productListFilterDto.Code != null)
                    {
                        productListDto = productListDto.Where(x => x.Code.Trim().ToLower() == productListFilterDto.Code.Trim().ToLower()).ToList();
                    }
                    var pagingSize = productListFilterDto.PagingFilterDto.Size;
                    var pageNumber = productListFilterDto.PagingFilterDto.Page;

                    pageNumber = pageNumber <= 1 ? 1 : pageNumber;

                    var totalItem = productListDto.Count;
                    var totalPage = (int)Math.Ceiling((double)totalItem / pagingSize);// Burada bölmeyi düzeltiliyor

                    var resultDto = new ProductListPagingDto
                    {
                        Data = productListDto,
                        Page = new PagingDto
                        {
                            Page = pageNumber,
                            Size = pagingSize,
                            TotalPages = totalPage, // Toplam sayfa sayısı burada kullanılıyor
                            TotalCount = totalItem,
                        }
                    };
                    return new SuccessDataResult<ProductListPagingDto>(resultDto, "oK", Messages.success);
                }
                return new ErrorDataResult<ProductListPagingDto>(new ProductListPagingDto(), "product not found", Messages.product_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ProductListPagingDto>(new ProductListPagingDto(),ex.Message,Messages.err_null);
            }
        }

        public IDataResult<ProductListDto> GetProductById(int productId)
        {
            try
            {
                var getProduct = _productDal.Get(x => x.Id == productId);
                if (getProduct == null)
                {
                    return new ErrorDataResult<ProductListDto>(null, "product not found", Messages.err_null);
                }

                var productList = new ProductListDto
                {
                    Id = productId,
                    Name = getProduct.Name,
                    Code = getProduct.Code,
                    CreatedDate = getProduct.CreatedDate,
                    ImageUrl = getProduct.ImageUrl,
                    Price = getProduct.Price,
                    Status = getProduct.Status
                };
                return new SuccessDataResult<ProductListDto>(productList, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ProductListDto>(null, ex.Message, Messages.err_null);
            }
        }

        public IDataResult<bool> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            try
            {
                if (productUpdateDto != null)
                {
                    var productUpd = _productDal.Get(x => x.Id == productUpdateDto.ProductId);
                    if (productUpd == null)
                    {
                        return new ErrorDataResult<bool>(false, "products not found", Messages.err_null);
                    }
                    productUpd.Name = productUpdateDto.Name;
                    productUpd.Code = productUpdateDto.Code;
                    productUpd.Price = productUpdateDto.Price;
                    _productDal.Update(productUpd);
                    return new SuccessDataResult<bool>(true, "product status updated successfully", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "dto cannot be empty", Messages.err_null);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.err_null);
            }
        }
    }
}
