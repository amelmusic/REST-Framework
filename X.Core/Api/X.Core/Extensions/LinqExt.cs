using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace X.Core.Extensions
{
    public static class LinqExt
    {
        public static bool Unique<TSource, T, V>(this IQueryable<TSource> query, Expression<Func<T>> primaryKey, Expression<Func<V>> field)
        {
            var member = (MemberExpression)primaryKey.Body;
            string propertyName = member.Member.Name;
            T value = primaryKey.Compile()();

            var memberVal = (MemberExpression)field.Body;
            string propertyNameVal = memberVal.Member.Name;
            V valueVal = field.Compile()();


            var result = query.Any($"it.{propertyName} != @0 && it.{propertyNameVal} == @1", value, valueVal);

            return result;
        }

        public static bool Overlaps<TSource, T, V>(this IQueryable<TSource> query, Expression<Func<T>> primaryKey, Expression<Func<DateTime>> validFrom, Expression<Func<DateTime?>> validTo, Expression<Func<V>> field)
        {
            var primaryKeyMember = (MemberExpression)primaryKey.Body;
            string primaryKeyPropName = primaryKeyMember.Member.Name;
            T primaryKeyValue = primaryKey.Compile()();

            var memberVal = (MemberExpression)field.Body;
            string propertyNameVal = memberVal.Member.Name;
            V valueVal = field.Compile()();

            var validToMember = (MemberExpression)validTo.Body;
            string validToPropName = validToMember.Member.Name;
            var validToValue = validTo.Compile()();

            var validFromMember = (MemberExpression)validFrom.Body;
            string validFromPropName = validFromMember.Member.Name;
            var validFromValue = validFrom.Compile()();
            //bool isOverlapping = query.Any(x =>
            //    x.Code == castedEntity.Code && x.Id != castedEntity.Id && x.ValidTo.HasValue
            //    && x.ValidFrom <= (castedEntity.ValidTo ?? castedEntity.ValidFrom)
            //    && (x.ValidTo) >= castedEntity.ValidFrom);

            var validFromOrValidTo = validToValue;
            if (validFromOrValidTo == null)
            {
                validFromOrValidTo = validFromValue;
            }
            var result = query.Any($@"it.{primaryKeyPropName} != @0 
                    && it.{propertyNameVal} == @1
                    && it.{validToPropName}.HasValue
                    && it.{validFromPropName} <= @3
                    && it.{validToPropName} >= @2", primaryKeyValue, valueVal, validFromValue, validFromOrValidTo);

            return result;
        }
    }
}
