// SPDX-License-Identifier: GPL-2.0-only

using System;

namespace ByteSerialization.Components.Values.Primitives
{
    public class EnumComponent : PrimitiveComponent
    {
        private Type UnderlyingType { get; set; }
        
        protected override void GetFuncs()
        {
            if (Node.Type == null)
                return;

            UnderlyingType = Enum.GetUnderlyingType(Node.Type);

            if (Reader != null)
                Read = () => Enum.ToObject(Node.Type, Reader.GetFunc(UnderlyingType)());
            if (Writer != null)
                Write = obj => Writer.GetFunc(UnderlyingType)(Convert.ChangeType(obj, UnderlyingType));

            //Node.Size = Marshal.SizeOf(UnderlyingType);
        }
    }
}
