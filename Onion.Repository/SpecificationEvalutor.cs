using Microsoft.EntityFrameworkCore;
using Onion.Core.Entities;
using Onion.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Repository
{
    public class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
        {
            var Query = inputQuery;
            if (spec.Criteria != null)
            {
                Query = Query.Where(spec.Criteria);
            }
            if(spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDesc is not null)
            {
                Query = Query.OrderBy(spec.OrderByDesc);
            }
            if(spec.IsPaginationEnabled)
            {
                Query = Query.Skip(spec.Skip).Take(spec.Take);
            }

            Query = spec.Includes.Aggregate(Query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression  ));
            return Query;
        }
    }
}
