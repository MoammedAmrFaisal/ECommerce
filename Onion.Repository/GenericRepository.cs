using Microsoft.EntityFrameworkCore;
using Onion.Core.Entities;
using Onion.Core.Repositories;
using Onion.Core.Specifications;
using Onion.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OnionContext _dbContext;

        public GenericRepository(OnionContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region without specification
        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _dbContext.Products
                    .Include(p => p.ProductType)
                    .Include(p => p.ProductBrand)
                    .ToListAsync();
            }
            else
                return await _dbContext.Set<T>().ToListAsync();
        }
         public async Task<T> GetByIdAsync(int id)
         => await _dbContext.Set<T>().FindAsync(id);
        #endregion

        #region with specification
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecifications<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), Spec);
        }

        async Task<int> IGenericRepository<T>.GetCountWithSpecAsync(ISpecifications<T> spес)
        {
            return await ApplySpecification(spес).CountAsync(); ;
        }
        #endregion
    }
}
