// SPDX-License-Identifier: GPL-2.0-only

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
