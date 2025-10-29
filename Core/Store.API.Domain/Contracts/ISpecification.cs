using Store.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Contracts
{
    public interface ISpecification<Tkey,TEntity> where TEntity : BaseEntity<Tkey>
    {
         List<Expression<Func<TEntity,object>>> Includes { get; set; }
         Expression<Func<TEntity,bool>>? Criteria { get; set; }

         Expression<Func<TEntity,object>>? OrderBy { get; set; }
         Expression<Func<TEntity,object>>? OrderByDescending { get; set; }
         int Take { get; set; }
         int Skip { get; set; }
         bool IsPagination { get; set; }
    }
}
