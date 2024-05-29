// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Values.Composites.Records;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes
{
    public interface IBindingHelper
    {
        int GetValue(PropertyComponent property);
    }

    public static class BindingHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, IBindingHelper> dictionary =
            new ConcurrentDictionary<Type, IBindingHelper>();

        public static IBindingHelper GetBindingHelper(this Type helperType) => 
            dictionary.GetOrAdd(helperType, x => (IBindingHelper)Activator.CreateInstance(x));
    }
}
