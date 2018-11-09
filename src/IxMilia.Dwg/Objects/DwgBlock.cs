﻿namespace IxMilia.Dwg.Objects
{
    public partial class DwgBlock
    {
        internal override void PreWrite()
        {
            _noLinks = _subentityRef.IsEmpty;
        }

        internal override void PoseParse(BitReader reader, DwgObjectCache objectCache)
        {
            base.PoseParse(reader, objectCache);
            if (!_noLinks && !_subentityRef.IsEmpty && _subentityRef.Code != DwgHandleReferenceCode.SoftPointer)
            {
                throw new DwgReadException("Incorrect sub entity handle code.");
            }

            foreach (var reactorHandle in _reactorHandles)
            {
                if (reactorHandle.Code != DwgHandleReferenceCode.HardPointer)
                {
                    throw new DwgReadException("Incorrect reactor handle code.");
                }
            }

            if (_xDictionaryObjectHandle.Code != DwgHandleReferenceCode.SoftPointer)
            {
                throw new DwgReadException("Incorrect XDictionary object handle code.");
            }

            if (LayerHandle.Code != DwgHandleReferenceCode.SoftOwner)
            {
                throw new DwgReadException("Incorrect layer handle code.");
            }

            if (!_isLineTypeByLayer && (_lineTypeHandle.IsEmpty || _lineTypeHandle.Code != DwgHandleReferenceCode.SoftOwner))
            {
                throw new DwgReadException("Incorrect line type handle code.");
            }
        }
    }
}