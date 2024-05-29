// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Composites.Records;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes.Helpers
{
    public interface IAlignmentHelper
    {
        int GetAlignment(RecordComponent record);
    }

    public static class IAlignmentHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, IAlignmentHelper> dictionary = 
            new ConcurrentDictionary<Type, IAlignmentHelper>();

        public static IAlignmentHelper GetAlignmentHelper(this Type type) => 
            dictionary.GetOrAdd(type, x => (IAlignmentHelper)Activator.CreateInstance(x));
    }
}
