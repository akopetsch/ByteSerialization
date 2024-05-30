// SPDX-License-Identifier: MIT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Extensions
{
    internal static class LinqExtensions
    {
        internal static IEnumerable<T> Except<T>(this IEnumerable<T> first, T second) =>
            first.Except(new T[] { second });

        internal static IEnumerable<T> OfType<T>(this IEnumerable<T> source, Type type) =>
            source.Where(x => x?.GetType().Is(type) ?? false);
    }
}
