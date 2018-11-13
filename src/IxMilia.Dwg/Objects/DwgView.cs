﻿using System;

namespace IxMilia.Dwg.Objects
{
    public partial class DwgView : DwgObject
    {
        public DwgView(string name)
            : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null.");
            }

            Name = name;
        }

        internal override DwgHandleReferenceCode ExpectedNullHandleCode => DwgHandleReferenceCode.SoftOwner;

        internal override void PoseParse(BitReader reader, DwgObjectCache objectCache)
        {
            if (ViewControlHandle.Code != DwgHandleReferenceCode.HardPointer)
            {
                throw new DwgReadException("Incorrect view control object parent handle code.");
            }
        }
    }
}
