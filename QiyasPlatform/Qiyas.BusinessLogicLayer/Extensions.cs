﻿using System;
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

        public static Expression<Func<TElement, bool>> BuildOrExpression<TElement, TValue>(
        Expression<Func<TElement, TValue>> valueSelector,
        IEnumerable<TValue> values)
        {
            if (null == valueSelector)
                throw new ArgumentNullException("valueSelector");
            if (null == values)
                throw new ArgumentNullException("values");
            ParameterExpression p = valueSelector.Parameters.Single();

            if (!values.Any())
                return e => false;

            var equals = values.Select(value =>
                (Expression)Expression.Equal(
                     valueSelector.Body,
                     Expression.Constant(
                         value,
                         typeof(TValue)
                     )
                )
            );
            var body = equals.Aggregate<Expression>(
                     (accumulate, equal) => Expression.Or(accumulate, equal)
             );

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

    }
}
