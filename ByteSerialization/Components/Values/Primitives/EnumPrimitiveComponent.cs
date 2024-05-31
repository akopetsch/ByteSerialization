// SPDX-License-Identifier: MIT

using System;

namespace ByteSerialization.Components.Values.Primitives
{
    public class EnumPrimitiveComponent : PrimitiveComponent
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
