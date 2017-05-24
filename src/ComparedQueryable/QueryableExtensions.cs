using System.Collections.Generic;
using System.Linq;
using NaturalSort;

namespace ComparedQueryable
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> into an <see cref="IQueryable{T}"/> the <see cref="string"/> 
        /// properites of which can be naturally sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IQueryable<T> AsNaturalQueryable<T>(this IEnumerable<T> collection)
        {
            return collection.AsComparedQueryable(NaturalSortComparer.Instance);
        }

        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> into an <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TComparison">Will order all properties of this type within <see cref="T"/>.</typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer">Used for ordering operations.</param>
        /// <returns></returns>
        public static IQueryable<T> AsComparedQueryable<T, TComparison>(this IEnumerable<T> collection,
            IComparer<TComparison> comparer)
        {
            return new EnumerableQuery<T, TComparison>(collection, comparer);
        }
    }
}