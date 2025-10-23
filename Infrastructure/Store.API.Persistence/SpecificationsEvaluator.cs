using Microsoft.EntityFrameworkCore;
using Store.API.Domain.Contracts;
using Store.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Persistence
{
    public static class SpecificationsEvaluator
    {
        // _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P=>P.Id==key as int?) as TEntity;

        public static IQueryable<TEntity> GetQuery<TKey,TEntity>(IQueryable<TEntity> inputquery ,ISpecification<TKey,TEntity> spec) where TEntity:BaseEntity<TKey>
        {
            IQueryable<TEntity> query = inputquery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); //_context.TEntity.where(spec.Crieteria)
            }
            query = spec.Includes.Aggregate(query, (query, include) => query.Include(include));

            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            } else if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

           
            return query;
        }
    }
}
