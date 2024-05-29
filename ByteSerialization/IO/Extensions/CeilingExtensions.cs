﻿// SPDX-License-Identifier: GPL-2.0-only

using System;

namespace ByteSerialization.IO.Extensions
{
    public static class CeilingExtensions
    {
        public static short Ceiling(this short value, short stepSize) =>
            Convert.ToInt16(Ceiling((long)value, stepSize));

        public static int Ceiling(this int value, int stepSize) =>
            Convert.ToInt32(Ceiling((long)value, stepSize));

        public static long Ceiling(this long value, long stepSize) => 
            Convert.ToInt64(Math.Ceiling((decimal)value / stepSize) * stepSize);
    }
}
