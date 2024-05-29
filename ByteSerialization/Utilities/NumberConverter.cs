// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;

namespace ByteSerialization.Utilities
{
    public class NumberConverter
    {
        private static readonly Dictionary<Type, Func<object, object>> converterByOutputType =
            new Dictionary<Type, Func<object, object>>() {
                { typeof(int), n => System.Convert.ToInt32(n) },
                { typeof(short), n => System.Convert.ToInt16(n) },
                // TODO: support more number types
            };

        private Func<object, object> converter;

        public Type OutputType { get; }

        public NumberConverter(Type outputType)
        {
            OutputType = outputType;
            converter = converterByOutputType[OutputType];
        }

        public object Convert(object number) =>
            converter(number);
    }
}
