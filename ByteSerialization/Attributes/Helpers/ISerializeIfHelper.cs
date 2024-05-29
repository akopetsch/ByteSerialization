﻿// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes.Helpers
{
    public interface ISerializeIfHelper
    {
        bool IsSerialized(Node node);
    }

    public static class ISerializeIfHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, ISerializeIfHelper> dictionary =
            new ConcurrentDictionary<Type, ISerializeIfHelper>();

        public static ISerializeIfHelper GetSerializeIfHelper(this Type helperType) => 
            dictionary.GetOrAdd(helperType, x => (ISerializeIfHelper)Activator.CreateInstance(x));
    }
}
