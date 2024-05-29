// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Values.Composites.Records;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes.Helpers
{
    public interface ITypeHelper
    {
        Type GetPropertyType(RecordComponent recordNode);
    }

    public static class ITypeHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, ITypeHelper> dictionary = 
            new ConcurrentDictionary<Type, ITypeHelper>();

        public static ITypeHelper GetTypeHelper(this Type helperType) => 
            dictionary.GetOrAdd(helperType, x => (ITypeHelper)Activator.CreateInstance(x));
    }
}
