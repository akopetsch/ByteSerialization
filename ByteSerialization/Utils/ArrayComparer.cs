// SPDX-License-Identifier: MIT

using System;
using System.Collections;

namespace ByteSerialization.Utils
{
    public static class ArrayComparer
    {
        public static bool AreEqual(Array array1, Array array2)
        {
            if (array1 == null || array2 == null)
                return array1 == array2;

            if (array1.Rank != array2.Rank)
                return false;

            for (int i = 0; i < array1.Rank; i++)
            {
                if (array1.GetLength(i) != array2.GetLength(i))
                    return false;
            }

            IEnumerator enumerator1 = array1.GetEnumerator();
            IEnumerator enumerator2 = array2.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                var value1 = enumerator1.Current;
                var value2 = enumerator2.Current;

                if (!AreElementsEqual(value1, value2))
                    return false;
            }

            return true;
        }

        private static bool AreElementsEqual(object value1, object value2)
        {
            if (value1 == null || value2 == null)
                return value1 == value2;

            if (value1.GetType() != value2.GetType())
                return false;

            if (value1 is IComparable comparableValue1)
                return comparableValue1.CompareTo(value2) == 0;

            return value1.Equals(value2);
        }
    }
}
