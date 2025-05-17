using ShopMarket.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Infrastrcuture.Specification.ProductWithSpecification
{
    public class ProductWithCatgeoryAndBrandAndTypeSpecs: Specification<Product>
    {
        public ProductWithCatgeoryAndBrandAndTypeSpecs(ProductSpecification product):base
            (c =>
            (!product.Category.HasValue || c.CategoryId == product.Category) &&
            (!product.Brand.HasValue || c.BrandId == product.Brand) &&
            (string.IsNullOrEmpty(product.Search) || c.Name.Trim().ToLower().Contains(product.Search))
            )

           {

            AddInclude(c => c.Category);     
            AddInclude(b => b.Brand);
            AddOrderBy(o => o.Name);

            if (!string.IsNullOrEmpty(product.Sort))
            {
                switch (product.Sort)
                {
                    case "PriceAsc"
                    :
                        AddOrderBy(o => o.Price);
                        break;
                    case "PriceDesc"
                    :
                        AddOrderByDesc(o => o.Price);
                        break;
                    default
                    :
                        AddOrderBy(o => o.Name);
                        break;
                }
            }

            AddPagination(product.PageSize * (product.PageIndex - 1), product.PageSize);

        }
        public ProductWithCatgeoryAndBrandAndTypeSpecs(int? productId) : base(c => c.Id == productId)
        {
            AddInclude(c => c.Category);

            AddInclude(b => b.Brand);
        }
    }
}
