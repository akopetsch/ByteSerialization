// SPDX-License-Identifier: MIT

namespace ByteSerialization.IO
{
    internal static class HashCodeHelper
    {
        internal static int CombineHashCodes<T>(params T[] objects)
        {
            var hashCode = new HashCode();
            if (objects != null)
                foreach (T obj in objects)
                    hashCode.Add(obj);
            return hashCode.ToHashCode();
        }
    }
}
