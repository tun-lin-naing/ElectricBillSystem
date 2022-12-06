using System.Linq.Expressions;

namespace ElectricBillWebApp.Common
{
    public static class SQlHelper
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }
    }
}
