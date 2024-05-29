// SPDX-License-Identifier: GPL-2.0-only

using System;

namespace ByteSerialization.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderAttribute : Attribute
    {
        public int Order { get; }

        public OrderAttribute(int order) => 
            Order = order;
    }
}
