﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes.Helpers
{
    public interface IReferenceHelper
    {
        bool IsReference(PropertyComponent propertyComponent);
        ReferenceConfiguration GetReferenceConfiguration(PropertyComponent propertyComponent);
    }

    public static class IReferenceHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, IReferenceHelper> dictionary =
            new ConcurrentDictionary<Type, IReferenceHelper>();

        public static IReferenceHelper GetReferenceHelper(this Type helperType) => 
            dictionary.GetOrAdd(helperType, x => (IReferenceHelper)Activator.CreateInstance(x));
    }
}
