// SPDX-License-Identifier: MIT

namespace ByteSerialization.Attributes.Offset
{
    public class OffsetComponent : AttributeComponent<OffsetAttribute>
    {
        #region Properties

        public int Offset => Attribute.Value;

        #endregion

        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Node.OnSerializing += EnsureOffset;
            Node.BeforeDeserializing += EnsureOffset;
        }

        private void EnsureOffset() =>
            Context.EnsureOffsetFrom(Offset, Parent);

        #endregion
    }
}
