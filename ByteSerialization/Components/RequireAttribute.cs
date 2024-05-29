﻿// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Extensions;
using System;

namespace ByteSerialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireAttribute : Attribute
    {
        public Type ComponentType { get; set; }

        public RequireAttribute(Type componentType)
        {
            if (!(ComponentType = componentType).Is<Component>())
                throw new InvalidOperationException();
        }
    }
}
