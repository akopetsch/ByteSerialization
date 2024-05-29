// SPDX-License-Identifier: MIT

// TODO: fix namespace
namespace ByteSerialization.Attributes
{
    public enum ReferenceHandling
    {
        HighPriority = 1,
        DefaultPriority = 0,
        LowPriority = -1,
        Postpone = -2,
        ForceReuse = -3,
    }
}
