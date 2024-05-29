// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using System;

namespace ByteSerialization.Components.Values.Primitives
{
    public class NullablePrimitiveComponent : PrimitiveComponent
    {
        private Type UnderlyingType { get; set; }

        protected override void GetFuncs()
        {
            if (Node.Type == null)
                return;

            UnderlyingType = Nullable.GetUnderlyingType(Node.Type);

            if (Reader != null)
                Read = () => 
                Reader.GetFunc(UnderlyingType)();
            if (Writer != null)
                Write = obj => Writer.GetFunc(UnderlyingType)(Convert.ChangeType(obj, UnderlyingType));

            //Node.Size = Marshal.SizeOf(UnderlyingType);
        }
    }
}
