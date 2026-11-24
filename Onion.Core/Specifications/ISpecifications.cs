using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        // where condition for filtering the data
        public Expression<Func<T, bool>> Criteria { get; set; }
        // list of includes
        public List<Expression<Func<T, object>>> Includes { get; set; }
        // prop for Order by (p=>p.name)
        public Expression<Func<T, object>> OrderBy { get; set; }
        // prop for Order by (p=>p.name) DESC
        public Expression<Func<T, object>> OrderByDesc { get; set; }


        //Take
        public int Take { get; set; }
        //Skip
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
