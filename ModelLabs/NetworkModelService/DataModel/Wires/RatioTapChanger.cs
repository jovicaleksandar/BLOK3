using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RatioTapChanger : TapChanger
    {
        public RatioTapChanger(long globalId) : base(globalId)
        {
        }

        private float stepVoltageIncrement;
        private TransformerControlMode tculControlMode;
        private List<long> transformerEnds = new List<long>();
        

        public float StepVoltageIncrement
        {
            get
            {
                return stepVoltageIncrement;
            }

            set
            {
                stepVoltageIncrement = value;
            }
        }

        public TransformerControlMode TculControlMode
        {
            get
            {
                return tculControlMode;
            }

            set
            {
                tculControlMode = value;
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
                RatioTapChanger x = (RatioTapChanger)obj;
                return (x.tculControlMode == this.tculControlMode &&
                        x.stepVoltageIncrement == this.stepVoltageIncrement &&
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
                case ModelCode.RATIOTAPCHANGER_STEPVOLTAGEINC:
                case ModelCode.RATIOTAPCHANGER_TCULCONTROLMODE:
                case ModelCode.RATIOTAPCHANGER_TREND:

                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.RATIOTAPCHANGER_STEPVOLTAGEINC:
                    property.SetValue(stepVoltageIncrement);
                    break;

                case ModelCode.RATIOTAPCHANGER_TCULCONTROLMODE:
                    property.SetValue((short)tculControlMode);
                    break;

                case ModelCode.RATIOTAPCHANGER_TREND:
                    property.SetValue(transformerEnds);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.RATIOTAPCHANGER_STEPVOLTAGEINC:
                    stepVoltageIncrement = property.AsFloat();
                    break;

                case ModelCode.RATIOTAPCHANGER_TCULCONTROLMODE:
                    tculControlMode = (TransformerControlMode)property.AsEnum();
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
                references[ModelCode.RATIOTAPCHANGER_TREND] = transformerEnds.GetRange(0, transformerEnds.Count);
            }

            base.GetReferences(references, refType);
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TRANSFORMEREND_RATIOTAPCHNG:
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
                case ModelCode.TRANSFORMEREND_RATIOTAPCHNG:

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
