using System;
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
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<T> AsNaturalQueryable<T>(this IEnumerable<T> source)
        {
            return source.AsComparedQueryable(NaturalSortComparer.Instance);
        }

        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> into an <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TComparison">Will order all properties of this type within <see cref="T"/>.</typeparam>
        /// <param name="source"></param>
        /// <param name="comparer">Used for ordering operations.</param>
        /// <returns></returns>
        public static IQueryable<T> AsComparedQueryable<T, TComparison>(this IEnumerable<T> source,
            IComparer<TComparison> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            // If the IQueryable is already is in the form of our own IQueryable implementation, there's no need to make
            // another one.
            var query = source as EnumerableQuery<T, TComparison>;
            return query ?? new EnumerableQuery<T, TComparison>(source, comparer);
        }
    }
}