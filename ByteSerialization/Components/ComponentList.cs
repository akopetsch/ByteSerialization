// SPDX-License-Identifier: MIT

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
