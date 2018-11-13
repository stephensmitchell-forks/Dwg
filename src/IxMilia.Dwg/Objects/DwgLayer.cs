﻿using System;
using System.Collections.Generic;

namespace IxMilia.Dwg.Objects
{
    public partial class DwgLayer : DwgObject
    {
        public DwgLineType LineType { get; internal set; }

        public IList<DwgEntity> Entities { get; private set; } = new List<DwgEntity>();

        internal override IEnumerable<DwgObject> ChildItems
        {
            get
            {
                yield return LineType;
                foreach (var entity in Entities)
                {
                    yield return entity;
                }
            }
        }

        internal override DwgHandleReferenceCode ExpectedNullHandleCode => DwgHandleReferenceCode.SoftOwner;

        public DwgLayer(string name)
            : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null.");
            }

            Name = name;
        }

        internal override void PreWrite()
        {
            _lineTypeHandle = new DwgHandleReference(DwgHandleReferenceCode.SoftOwner, LineType.Handle.HandleOrOffset);
            foreach (var entity in Entities)
            {
                entity.LayerHandle = new DwgHandleReference(DwgHandleReferenceCode.SoftOwner, Handle.HandleOrOffset);
            }
        }

        internal override void PoseParse(BitReader reader, DwgObjectCache objectCache)
        {
            if (LayerControlHandle.Code != DwgHandleReferenceCode.HardPointer)
            {
                throw new DwgReadException("Incorrect layer control object parent handle code.");
            }

            if (_lineTypeHandle.Code != DwgHandleReferenceCode.SoftOwner)
            {
                throw new DwgReadException("Incorrect layer line type handle code.");
            }

            LineType = objectCache.GetObject<DwgLineType>(reader, _lineTypeHandle.HandleOrOffset);
        }
    }
}
