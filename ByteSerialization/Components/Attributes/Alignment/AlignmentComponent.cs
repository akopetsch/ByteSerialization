// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Values.Composites.Records;

namespace ByteSerialization.Attributes.Alignment
{
    public class AlignmentComponent : AttributeComponent<AlignmentAttribute>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Node.OnSerializing += EnsureAlignment;
        }

        private void EnsureAlignment()
        {
            int alignment = Attribute.Value ?? 
                Attribute.Helper.GetAlignment((RecordComponent)Target);
            Context.EnsureAlignment(alignment);
        }
    }
}
