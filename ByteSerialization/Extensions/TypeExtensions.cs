// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool Is<T>(this Type type) =>
            typeof(T).IsAssignableFrom(type);

        internal static bool Is(this Type type, Type otherType) => 
            otherType.IsAssignableFrom(type);

        internal static bool IsOneOf<T1, T2>(this Type type) => 
            type.IsOneOf(typeof(T1), typeof(T2));

        internal static bool IsOneOf(this Type type, params Type[] otherTypes) => 
            otherTypes.Contains(type);

        internal static bool IsBuiltinList(this Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);

        internal static string GetFriendlyName(this Type type)
        {
            if (type.IsGenericType)
            {
                string name = type.Name.Split('`').First();
                return $"{name}<{string.Join(", ", type.GenericTypeArguments.Select(GetFriendlyName))}>";
            }
            else
            {
                // TODO: use CSharpCodeProvider
                //var compiler = new CSharpCodeProvider();
                //var type = new CodeTypeReference(typeof(ModelSerializationTest));
                //return compiler.GetTypeOutput(type);
                return type.Name;
            }
        }
    }
}
