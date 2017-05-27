using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ComparedQueryable.NativeQueryable;
using Queryable = System.Linq.Queryable;

namespace ComparedQueryable
{
    internal class EnumerableRewriter<TComparison> : EnumerableRewriter
    {
        private readonly IComparer<TComparison> m_comparer;
        private static readonly ISet<string> ComparerFunctions = new HashSet<string>(new[]
        {
            nameof(Queryable.OrderBy),
            nameof(Queryable.OrderByDescending),
            nameof(Queryable.ThenBy),
            nameof(Queryable.ThenByDescending)
        });

        static EnumerableRewriter()
        {
        }

        public EnumerableRewriter(IComparer<TComparison> comparer)
        {
            m_comparer = comparer;
        }

        protected override ReadOnlyCollection<Expression> FixupQuotedArgs(MethodInfo mi,
            ReadOnlyCollection<Expression> argList)
        {
            var queryableArgs = base.FixupQuotedArgs(mi, argList);
            return GetOrderByParameters(mi, argList, queryableArgs);
        }

        private ReadOnlyCollection<Expression> GetOrderByParameters(MethodInfo mi,
            IReadOnlyCollection<Expression> argList,
            ReadOnlyCollection<Expression> queryableArgs)
        {
            // If we're dealing with any other functions besides the ordering functions, let's do what we normally do
            // with IQueryables.
            if (!ComparerFunctions.Contains(mi.Name))
            {
                return queryableArgs;
            }

            // If the caller has already provided an IComparer argument (i.e. they called the IQueryable directly), let
            // them use the one they passed.
            var comparerExpresion = argList.LastOrDefault() as ConstantExpression;
            if (comparerExpresion != null
                && comparerExpresion.Type.IsGenericType
                && comparerExpresion.Type.GetGenericTypeDefinition() == typeof(IComparer<>))
            {
                return queryableArgs;
            }

            var operandExpression = argList
                .Where(expression => expression is UnaryExpression)
                .Cast<UnaryExpression>()
                .FirstOrDefault();
            // If the ordering expression isn't a simple ordering expression (i.e. x => x.PropertyName), let's do what
            // we normally do.
            if (operandExpression == null)
            {
                return queryableArgs;
            }
            // If the ordering expression is ordering by a different type than the type found in the comparing function,
            // let's order as we normally do.
            if (operandExpression.Operand.Type.GetGenericArguments().All(type => type != typeof(TComparison)))
            {
                return queryableArgs;
            }
            // Since we've gotten this far, let's pass in the custom comparer to the ordering function.
            return queryableArgs.Concat(new[] { Expression.Constant(m_comparer) }).ToList().AsReadOnly();
        }

        protected override MethodInfo GetGenericMethod(Type[] typeArgs,
            MethodInfo matchingMethodInfo,
            IEnumerable<MethodInfo> potentialMethodMatches)
        {
            if (typeArgs.All(type => type != typeof(TComparison)))
            {
                return matchingMethodInfo.MakeGenericMethod(typeArgs);
            }

            var methodInfoWithComparer = potentialMethodMatches
                .FirstOrDefault(MethodInfoHasIComparerParameter);
            return (methodInfoWithComparer ?? matchingMethodInfo).MakeGenericMethod(typeArgs);
        }

        private static bool MethodInfoHasIComparerParameter(MethodInfo methodInfo)
        {
            return methodInfo
                .GetParameters()
                .Where(parameterInfo => parameterInfo.ParameterType.IsGenericType)
                .Any(parameterInfo => parameterInfo.ParameterType.GetGenericTypeDefinition() == typeof(IComparer<>));
        }
    }
}