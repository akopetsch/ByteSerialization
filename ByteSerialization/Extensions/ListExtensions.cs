// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Extensions
{
    public static class ListExtensions
    {
        public static void AddRangeIfAny<T>(this List<T> list, IEnumerable<T> collection)
        {
            if (collection.Any())
                list.AddRange(collection);
        }
    }
}
