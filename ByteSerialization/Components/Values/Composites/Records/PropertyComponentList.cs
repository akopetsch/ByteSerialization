// SPDX-License-Identifier: MIT

using ByteSerialization.Pooling;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Components.Values.Composites.Records
{
    public class PropertyComponentList : List<PropertyComponent>, IPoolable
    {
        #region Members (: IPoolable)

        public event ReleaseEventHandler OnRelease;

        void IPoolable.Release()
        {
            Clear();
            OnRelease();
        }

        #endregion

        public PropertyComponent this[string name] => 
            this.FirstOrDefault(p => p.Name.Equals(name));
    }
}
