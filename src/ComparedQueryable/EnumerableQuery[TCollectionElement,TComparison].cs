using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComparedQueryable.NativeQueryable;

namespace ComparedQueryable
{
    /// <inheritdoc />
    public class EnumerableQuery<TCollectionElement, TComparison> : NativeQueryable.EnumerableQuery<TCollectionElement>
    {
        private readonly IComparer<TComparison> m_comparer;

        /// <inheritdoc />
        public EnumerableQuery(IEnumerable<TCollectionElement> enumerable,
            IComparer<TComparison> comparer) : base(enumerable)
        {
            m_comparer = comparer;
        }

        /// <inheritdoc />
        public EnumerableQuery(Expression expression, IComparer<TComparison> comparer) : base(expression)
        {
            m_comparer = comparer;
        }

        internal override IQueryable Create(Type elementType, Expression expression)
        {
            var seqType = typeof(EnumerableQuery<,>).MakeGenericType(elementType, typeof(TComparison));
            return (IQueryable)Activator.CreateInstance(seqType, expression, m_comparer);
        }

        protected override IQueryable<TElement> GetEnumerableQuery<TElement>(Expression expression)
        {
            return new EnumerableQuery<TElement, TComparison>(expression, m_comparer);
        }

        protected override NativeQueryable.EnumerableExecutor<TElement> GetEnumerableExecutor<TElement>(
            Expression expression)
        {
            return new EnumerableExecutor<TElement, TComparison>(expression, m_comparer);
        }

        internal override EnumerableRewriter GetEnumerableRewriter()
        {
            return new EnumerableRewriter<TComparison>(m_comparer);
        }
    }
}