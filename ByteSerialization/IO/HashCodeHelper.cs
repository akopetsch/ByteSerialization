// SPDX-License-Identifier: MIT

using System;

namespace ByteSerialization.IO
{
    public static class HashCodeHelper
    {
        public static int CombineHashCodes<T>(params T[] objects)
        {
            var hashCode = new HashCode();
            if (objects != null)
                foreach (T obj in objects)
                    hashCode.Add(obj);
            return hashCode.ToHashCode();
        }
    }
}
