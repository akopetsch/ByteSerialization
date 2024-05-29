// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace ByteSerialization.Components.Attributes.Reference
{
    public class ReferenceConfiguration
    {
        public int? Alignment { get; set; } = 4;
        public ReferenceHandling Handling { get; set; } = ReferenceHandling.DefaultPriority;
        public int Order { get; set; } = 0;
    }
}
