﻿// SPDX-License-Identifier: MIT

using System;

namespace ByteSerialization.Utils
{
    public static class CeilingHelper
    {
        public static short Ceiling(this short value, short stepSize) =>
            Convert.ToInt16(Ceiling((long)value, stepSize));

        public static int Ceiling(this int value, int stepSize) =>
            Convert.ToInt32(Ceiling((long)value, stepSize));

        public static long Ceiling(this long value, long stepSize) =>
            Convert.ToInt64(Math.Ceiling((decimal)value / stepSize) * stepSize);
    }
}
