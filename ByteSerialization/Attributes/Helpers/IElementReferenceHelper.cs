﻿// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Collections;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes.Helpers
{
    public interface IElementReferenceHelper
    {
        ReferenceConfiguration GetReferenceConfiguration(CollectionComponent c, Type elementType);
    }

    public static class IElementReferenceHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, IElementReferenceHelper> dictionary = 
            new ConcurrentDictionary<Type, IElementReferenceHelper>();

        public static IElementReferenceHelper GetElementReferenceHelper(this Type helperType) => 
            dictionary.GetOrAdd(helperType, x => (IElementReferenceHelper)Activator.CreateInstance(x));
    }
}
