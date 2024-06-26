﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Composites.Collections;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes.Helpers
{
    public interface IElementTypeHelper
    {
        Type GetElementType(CollectionComponent collection);
    }

    public static class IElementTypeHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, IElementTypeHelper> dictionary = 
            new ConcurrentDictionary<Type, IElementTypeHelper>();

        public static IElementTypeHelper GetElementTypeHelper(this Type helperType) => 
            dictionary.GetOrAdd(helperType, x => (IElementTypeHelper)Activator.CreateInstance(x));
    }
}
