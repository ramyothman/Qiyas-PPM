using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Qiyas.BusinessLogicLayer
{
    internal static class Extensions
    {
        internal static Expression<Func<TSource, bool>> AndAlso<TSource>(this Expression<Func<TSource, bool>> left, Expression<Func<TSource, bool>> right)
        {
            var param = Expression.Parameter(typeof(TSource), "x");
            var body = Expression.AndAlso(Expression.Invoke(left, param), Expression.Invoke(right, param));
            var lambda = Expression.Lambda<Func<TSource, bool>>(body, param);
            return lambda;
        }
    }
}
