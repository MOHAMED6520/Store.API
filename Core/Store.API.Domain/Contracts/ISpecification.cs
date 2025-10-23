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
        public List<Expression<Func<TEntity,object>>> Includes { get; set; }
        public Expression<Func<TEntity,bool>>? Criteria { get; set; }
        public Expression<Func<TEntity,object>>? OrderBy { get; set; }
        public Expression<Func<TEntity,object>>? OrderByDescending { get; set; }
    }
}
