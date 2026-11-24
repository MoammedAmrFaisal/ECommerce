using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Specifications
{
    public class ProductWithBrandAndTypeSpec : BaseSpecifications<Product>
    {
        // Constructor for GetAll
        public ProductWithBrandAndTypeSpec(ProductSpecParams Params)
            : base(p =>
                (string.IsNullOrEmpty(Params.Search) || 
                p.Name.ToLower().Contains(Params.Search.ToLower()))
                &&
                (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)
                &&
                (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId))
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        SetOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        SetOrderByDesc(p => p.Price);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;

                }
            }
            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
        }

        // Constructor for GetById
        public ProductWithBrandAndTypeSpec(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
