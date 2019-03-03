using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TapChanger : PowerSystemResource
    {
        private int highStep;
        private int lowStep;
        private int neutralStep;
        private int normalStep;
        private float initialDelay;
        private float subsequentDelay;
        private float neutralU;
        private bool ltcFlag;
        private bool regulationStatus;

        public TapChanger(long globalId) : base(globalId)
        {
        }

        public bool LtcFlag
        {
            get
            {
                return ltcFlag;
            }

            set
            {
                ltcFlag = value;
            }
        }

        public bool RegulationStatus
        {
            get
            {
                return regulationStatus;
            }

            set
            {
                regulationStatus = value;
            }
        }
        public int HighStep
        {
            get
            {
                return highStep;
            }

            set
            {
                highStep = value;
            }
        }
        public int LowStep
        {
            get
            {
                return lowStep;
            }

            set
            {
                lowStep = value;
            }
        }
        public int NeutralStep
        {
            get
            {
                return neutralStep;
            }

            set
            {
                neutralStep = value;
            }
        }
        public int NormalStep
        {
            get
            {
                return normalStep;
            }

            set
            {
                normalStep = value;
            }
        }
        public float InitialDelay
        {
            get
            {
                return initialDelay;
            }

            set
            {
                initialDelay = value;
            }
        }
        public float SubsequentDelay
        {
            get
            {
                return subsequentDelay;
            }

            set
            {
                subsequentDelay = value;
            }
        }
        public float NeutralU
        {
            get
            {
                return neutralU;
            }

            set
            {
                neutralU = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapChanger x = (TapChanger)obj;
                return ((x.highStep == this.highStep) && (x.lowStep == this.lowStep) && (x.normalStep == this.normalStep) && (x.initialDelay == this.initialDelay) &&
                        (x.neutralStep == this.neutralStep) && (x.subsequentDelay == this.subsequentDelay) && (x.highStep == this.highStep) &&
                        (x.neutralU == this.neutralU) && (x.regulationStatus == this.regulationStatus) && (x.ltcFlag == this.ltcFlag));
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.TAPCHANGER_HIGHSTEP:
                case ModelCode.TAPCHANGER_LOWSTEP:
                case ModelCode.TAPCHANGER_NORMALSTEP:
                case ModelCode.TAPCHANGER_NEUTRALSTEP:
                case ModelCode.TAPCHANGER_NEUTRALU:
                case ModelCode.TAPCHANGER_INITIALDELAY:
                case ModelCode.TAPCHANGER_SUBSEQUENTDELAY:
                case ModelCode.TAPCHANGER_REGULATIONSTATUS:
                case ModelCode.TAPCHANGER_LTCFLAG:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TAPCHANGER_HIGHSTEP:
                    property.SetValue(highStep);
                    break;

                case ModelCode.TAPCHANGER_LOWSTEP:
                    property.SetValue(lowStep);
                    break;

                case ModelCode.TAPCHANGER_NORMALSTEP:
                    property.SetValue(normalStep);
                    break;

                case ModelCode.TAPCHANGER_NEUTRALSTEP:
                    property.SetValue(neutralStep);
                    break;

                case ModelCode.TAPCHANGER_NEUTRALU:
                    property.SetValue(neutralU);
                    break;

                case ModelCode.TAPCHANGER_INITIALDELAY:
                    property.SetValue(initialDelay);
                    break;

                case ModelCode.TAPCHANGER_SUBSEQUENTDELAY:
                    property.SetValue(subsequentDelay);
                    break;

                case ModelCode.TAPCHANGER_REGULATIONSTATUS:
                    property.SetValue(regulationStatus);
                    break;

                case ModelCode.TAPCHANGER_LTCFLAG:
                    property.SetValue(ltcFlag);
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
                case ModelCode.TAPCHANGER_HIGHSTEP:
                    highStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_LOWSTEP:
                    lowStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_NORMALSTEP:
                    normalStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_NEUTRALSTEP:
                    neutralStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_NEUTRALU:
                    neutralU = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_INITIALDELAY:
                    initialDelay = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_SUBSEQUENTDELAY:
                    subsequentDelay = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_REGULATIONSTATUS:
                    regulationStatus = property.AsBool();
                    break;
                case ModelCode.TAPCHANGER_LTCFLAG:
                    ltcFlag = property.AsBool();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}

