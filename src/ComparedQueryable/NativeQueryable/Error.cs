// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace ComparedQueryable.NativeQueryable
{
    internal static class Error
    {
        internal static Exception ArgumentNull(string message) => new ArgumentNullException(message);

        internal static Exception ArgumentNotIEnumerableGeneric(string message) =>
            new ArgumentException(message);

        internal static Exception ArgumentNotValid(string message) =>
            new ArgumentException(message);

        internal static Exception ArgumentOutOfRange(string message) =>
            new ArgumentOutOfRangeException(message);

        internal static Exception NoMethodOnType(string name, object type) =>
            new InvalidOperationException($"No method named \"{name}\" on type \"{type}\".");

        internal static Exception NoMethodOnTypeMatchingArguments(string name, object type) =>
            new InvalidOperationException($"No method named \"{name}\" on type \"{type}\" found with matching " +
                "arguments.");

        internal static Exception EnumeratingNullEnumerableExpression() =>
            new InvalidOperationException("The Enumerable you're enumerating is null.");
    }
}