// SPDX-License-Identifier: MIT

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
