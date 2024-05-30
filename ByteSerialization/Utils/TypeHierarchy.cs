// SPDX-License-Identifier: MIT

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Utils
{
    public class TypeHierarchy : List<Type>
    {
        public Type BaseType => this.First();
        public Type ConcreteType => this.Last();

        public TypeHierarchy(Type t)
        {
            while (
                t != null && 
                t != typeof(object))
            {
                Insert(0, t);
                t = t.BaseType;
            }
        }
    }

    public static class TypeHierarchyExtensions
    {
        private static readonly ConcurrentDictionary<Type, TypeHierarchy> dictionary = 
            new ConcurrentDictionary<Type, TypeHierarchy>();

        public static TypeHierarchy GetHierarchy(this Type type) =>
            dictionary.GetOrAdd(type, x => new TypeHierarchy(x));
    }
}
