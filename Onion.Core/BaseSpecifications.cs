using Onion.Core.Entities;
using Onion.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        // Constructor for GetAll
        public BaseSpecifications()
        {
            Includes = new List<Expression<Func<T, object>>>();
        }
        // ctor for GetById
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            Includes = new List<Expression<Func<T, object>>>();
        }

        public void SetOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        public void SetOrderByDesc(Expression<Func<T, object>> OrderByExpressionDesc)
        {
            OrderByDesc = OrderByExpressionDesc;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip= skip;
            Take= take;
        }

    }
}
