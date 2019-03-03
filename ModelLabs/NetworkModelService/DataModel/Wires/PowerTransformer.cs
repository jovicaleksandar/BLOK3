using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PowerTransformer : ConductingEquipment
    {
        private string vectorGroup = string.Empty;
        private List<long> powerTransformerEnds = new List<long>();

        public PowerTransformer(long globalId) : base(globalId) 
		{
        }

        public List<long> PowerTransformerEnds
        {
            get
            {
                return powerTransformerEnds;
            }

            set
            {
                powerTransformerEnds = value;
            }
        }

        public string VectorGroup
        {
            get { return vectorGroup; }
            set { vectorGroup = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PowerTransformer x = (PowerTransformer)obj;
                return (x.vectorGroup == this.vectorGroup && CompareHelper.CompareLists(x.powerTransformerEnds, this.powerTransformerEnds, true));
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
                case ModelCode.POWERTR_VECTORGROUP:
                case ModelCode.POWERTR_POWERTREND:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.POWERTR_VECTORGROUP:
                    prop.SetValue(vectorGroup);
                    break;

                case ModelCode.POWERTR_POWERTREND:
                    prop.SetValue(powerTransformerEnds);
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
                case ModelCode.POWERTR_VECTORGROUP:
                    vectorGroup = property.AsString();
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
                return powerTransformerEnds.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (powerTransformerEnds != null && powerTransformerEnds.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.POWERTR_POWERTREND] = powerTransformerEnds.GetRange(0, powerTransformerEnds.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.POWTREND_POWTR:
                    powerTransformerEnds.Add(globalId);
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
                case ModelCode.POWTREND_POWTR:

                    if (powerTransformerEnds.Contains(globalId))
                    {
                        powerTransformerEnds.Remove(globalId);
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
