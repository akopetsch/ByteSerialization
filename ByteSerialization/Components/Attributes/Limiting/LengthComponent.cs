// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Values.Composites.Collections;
using System.Collections;

namespace ByteSerialization.Attributes.Limiting.Length
{
    public class LengthComponent : BindingComponent<LengthAttribute>, ILimitingComponent
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: call SetValue
            //Node.BeforeSerializing += SetBindingValue;
        }

        private void SetBindingValue()
        {
            if (Value is ICollection collection)
            {
                int length = collection.Count;
                SetBindingValue(length);
            }
        }

        public bool IsDeserializedUntilEnd(CollectionComponent collection)
        {
            int actual = collection.Elements.Count;
            int target = GetBindingValue();
            return actual >= target;
        }
    }
}
