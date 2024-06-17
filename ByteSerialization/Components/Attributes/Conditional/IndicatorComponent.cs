// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Attributes.Conditional;
using ByteSerialization.Nodes;

namespace ByteSerialization.Attributes.Conditional
{
    public class IndicatorComponent : AttributeComponent<IndicatorAttribute>, IConditionalComponent
    {
        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Node.OnSerializing += WriteIndicator;
        }

        private void WriteIndicator() =>
            Writer.Write(Attribute.Value);

        public bool IsSerialized(Node node) =>
            Reader.TryRead(Attribute.Value);

        #endregion
    }
}
