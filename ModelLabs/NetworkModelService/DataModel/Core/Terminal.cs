﻿using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private bool connected;
        private PhaseCode phases;
        private int sequenceNumber;
        private long connectivityNode = 0;
        private long conductingEquipment = 0;

        private List<long> transformerEnds = new List<long>();

        public Terminal(long globalId) : base(globalId) 
		{
        }

        public bool Connected
        {
            get
            {
                return connected;
            }

            set
            {
                connected = value;
            }
        }
        public PhaseCode Phases
        {
            get
            {
                return phases;
            }

            set
            {
                phases = value;
            }
        }
        public int SequenceNumber
        {
            get
            {
                return sequenceNumber;
            }

            set
            {
                sequenceNumber = value;
            }
        }

        public long ConnectivityNode
        {
            get
            {
                return connectivityNode;
            }

            set
            {
                connectivityNode = value;
            }
        }

        public long ConductingEquipment
        {
            get
            {
                return conductingEquipment;
            }

            set
            {
                conductingEquipment = value;
            }
        }

        public List<long> TransformerEnds
        {
            get
            {
                return transformerEnds;
            }

            set
            {
                transformerEnds = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return ((x.Connected == this.Connected) && (x.Phases == this.Phases) && (x.SequenceNumber == this.SequenceNumber) &&
                        (x.ConnectivityNode == this.ConnectivityNode) && (x.ConductingEquipment == this.ConductingEquipment) &&
                        (CompareHelper.CompareLists(x.TransformerEnds, this.TransformerEnds)));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.TERMINAL_CONNECTED:
                case ModelCode.TERMINAL_PHASES:
                case ModelCode.TERMINAL_SEQNUM:
                case ModelCode.TERMINAL_CONNECTIVITYNODE:
                case ModelCode.TERMINAL_CONDEQ:
                case ModelCode.TERMINAL_TREND:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TERMINAL_CONNECTED:
                    prop.SetValue(connected);
                    break;
                case ModelCode.TERMINAL_PHASES:
                    prop.SetValue((short)phases);
                    break;
                case ModelCode.TERMINAL_SEQNUM:
                    prop.SetValue(sequenceNumber);
                    break;
                case ModelCode.TERMINAL_CONDEQ:
                    prop.SetValue(conductingEquipment);
                    break;
                case ModelCode.TERMINAL_CONNECTIVITYNODE:
                    prop.SetValue(connectivityNode);
                    break;
                case ModelCode.TERMINAL_TREND:
                    prop.SetValue(transformerEnds);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CONNECTED:
                    connected = property.AsBool();
                    break;
                case ModelCode.TERMINAL_PHASES:
                    phases = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.TERMINAL_SEQNUM:
                    sequenceNumber = property.AsInt();
                    break;
                case ModelCode.TERMINAL_CONNECTIVITYNODE:
                    connectivityNode = property.AsReference();
                    break;
                case ModelCode.TERMINAL_CONDEQ:
                    conductingEquipment = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation	

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return transformerEnds.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (transformerEnds != null && transformerEnds.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_TREND] = transformerEnds.GetRange(0, transformerEnds.Count);
            }

            if (connectivityNode != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONNECTIVITYNODE] = new List<long>();
                references[ModelCode.TERMINAL_CONNECTIVITYNODE].Add(connectivityNode);
            }

            if (conductingEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONDEQ] = new List<long>();
                references[ModelCode.TERMINAL_CONDEQ].Add(conductingEquipment);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TRANSFORMEREND_TERMINAL:
                    transformerEnds.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TRANSFORMEREND_TERMINAL:

                    if (transformerEnds.Contains(globalId))
                    {
                        transformerEnds.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation	

    }
}
