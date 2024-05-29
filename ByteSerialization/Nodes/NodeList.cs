// SPDX-License-Identifier: MIT

using ByteSerialization.Pooling;
using System.Collections.Generic;

namespace ByteSerialization.Nodes
{
    public class NodeList : List<Node>, IPoolable
    {
        public event ReleaseEventHandler OnRelease;
        
        void IPoolable.Release()
        {
            Clear();
            OnRelease();
        }
    }
}
