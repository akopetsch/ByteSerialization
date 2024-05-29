// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Nodes;
using System.Collections.Generic;

namespace ByteSerialization.Components
{
    [Require(typeof(ReferencesCollectorComponent))]
    public sealed class RootComponent : Component
    {
        #region Properties

        public ValueComponent ValueComponent { get; private set; }

        public List<ReferenceComponent> PostponedReferences { get; } = new List<ReferenceComponent>();
        public List<ReferenceComponent> ForceReuseReferences { get; } = new List<ReferenceComponent>();

        #endregion

        #region Methods

        public override void OnAdded()
        {
            base.OnAdded();

            ValueComponent = Node.AddValueComponent(Node.Type);
        }

        #endregion
    }
}
