using Onion.Core.Entities;
using Onion.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region without Specification
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        #endregion

        #region With Specification
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec);
        Task<int> GetCountWithSpecAsync(ISpecifications<T> spес);
        #endregion
    }
}
