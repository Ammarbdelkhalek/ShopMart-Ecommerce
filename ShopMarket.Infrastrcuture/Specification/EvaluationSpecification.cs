using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopMarket.Infrastrcuture.Specification
{
    public class EvaluationSpecification<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> Query, ISpecification<T> specification)
        {
            var query = Query.AsQueryable();

            if (specification.Creiteria != null)
                query = query.Where(specification.Creiteria);

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDescinding != null)
                query = query.OrderByDescending(specification.OrderByDescinding);

            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
