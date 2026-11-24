using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Specifications
{
    public class ProdctWithFilterationForCountAsync : BaseSpecifications <Product>
    {
        public ProdctWithFilterationForCountAsync(ProductSpecParams Params) : base(p =>
            (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
        )
        {
            
        }
    }
}
