﻿// SPDX-License-Identifier: MIT

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ByteSerialization.Attributes
{
    public class AttributeComponentFactory
    {
        #region Singleton

        public static AttributeComponentFactory Instance { get; } = new AttributeComponentFactory();
        private AttributeComponentFactory() { }

        #endregion

        #region Fields

        private readonly ConcurrentDictionary<Type, AttributeComponentAttribute> dictionary =
            new ConcurrentDictionary<Type, AttributeComponentAttribute>();

        #endregion

        #region Methods

        public Type GetComponentType(Type attributeType)
        {
            Type componentType = GetAttribute(attributeType)?.ComponentType;
            if (componentType == null)
            {
                string message = $"no component for attribute {attributeType.Name}";
                throw new InvalidOperationException(message);
            }
            else
                return componentType;
        }

        private AttributeComponentAttribute GetAttribute(Type attributeType) =>
            dictionary.GetOrAdd(attributeType, x => x.GetCustomAttribute<AttributeComponentAttribute>());

        #endregion
    }
}
