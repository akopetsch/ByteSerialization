// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Pooling;
using System.Collections.Generic;

namespace ByteSerialization.Components
{
    public class ComponentList : List<Component>, IPoolable
    {
        public event ReleaseEventHandler OnRelease;

        void IPoolable.Release()
        {
            Clear();
            OnRelease();
        }
    }

    //public class ComponentList : ListByType<Component>
    //{

    //}
}
