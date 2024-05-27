// Copyright 2024 Alexander Kopetsch
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
