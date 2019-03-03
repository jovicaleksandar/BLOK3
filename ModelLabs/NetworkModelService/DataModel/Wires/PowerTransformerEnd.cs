using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PowerTransformerEnd : TransformerEnd
    {
        private float b;
        private float b0;
        private float g;
        private float g0;
        private float r;
        private float r0;
        private float x;
        private float x0;
        private float ratedS;
        private float ratedU;
        private WindingConnection connectionKind;
        private int phaseAngleClock;

        private long powerTransformers = 0;

        public PowerTransformerEnd(long globalId) : base(globalId) 
		{
        }

        public long PowerTransformers
        {
            get
            {
                return powerTransformers;
            }

            set
            {
                powerTransformers = value;
            }
        }

        public float B
        {
            get { return b; }
            set { b = value; }
        }

        public float B0
        {
            get { return b0; }
            set { b0 = value; }
        }

        public float G
        {
            get { return g; }
            set { g = value; }
        }

        public float G0
        {
            get { return g0; }
            set { g0 = value; }
        }

        public float R
        {
            get { return r; }
            set { r = value; }
        }

        public float R0
        {
            get { return r0; }
            set { r0 = value; }
        }
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float X0
        {
            get { return x0; }
            set { x0 = value; }
        }
        public float RatedS
        {
            get { return ratedS; }
            set { ratedS = value; }
        }
        public float RatedU
        {
            get { return ratedS; }
            set { ratedS = value; }
        }

        public int PhaseAngleClock
        {
            get { return phaseAngleClock; }
            set { phaseAngleClock = value; }
        }

        public WindingConnection ConnectionKind
        {
            get { return connectionKind; }
            set { connectionKind = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PowerTransformerEnd x = (PowerTransformerEnd)obj;
                return (x.b == this.b && x.b0 == this.b0 && x.r == this.r && x.r0 == this.r0 && x.x == this.x && x.x0 == this.x0 &&
                    x.g0 == this.g0 && x.g == this.g && x.ratedS == this.ratedS && x.ratedU == this.ratedU && x.connectionKind == this.connectionKind &&
                    x.phaseAngleClock == this.phaseAngleClock && x.powerTransformers == this.powerTransformers);
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
                case ModelCode.POWTREND_B:
                case ModelCode.POWTREND_B0:
                case ModelCode.POWTREND_G:
                case ModelCode.POWTREND_G0:
                case ModelCode.POWTREND_R:
                case ModelCode.POWTREND_R0:
                case ModelCode.POWTREND_X:
                case ModelCode.POWTREND_X0:
                case ModelCode.POWTREND_RATEDS:
                case ModelCode.POWTREND_RATEDU:
                case ModelCode.POWTREND_CONNKIND:
                case ModelCode.POWTREND_PHASEANGLCLOCK:
                case ModelCode.POWTREND_POWTR:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.POWTREND_B:
                    prop.SetValue(b);
                    break;

                case ModelCode.POWTREND_B0:
                    prop.SetValue(b0);
                    break;

                case ModelCode.POWTREND_G:
                    prop.SetValue(g);
                    break;

                case ModelCode.POWTREND_G0:
                    prop.SetValue(g0);
                    break;

                case ModelCode.POWTREND_R:
                    prop.SetValue(r);
                    break;

                case ModelCode.POWTREND_R0:
                    prop.SetValue(r0);
                    break;

                case ModelCode.POWTREND_X:
                    prop.SetValue(x);
                    break;

                case ModelCode.POWTREND_X0:
                    prop.SetValue(x0);
                    break;

                case ModelCode.POWTREND_RATEDS:
                    prop.SetValue(ratedS);
                    break;

                case ModelCode.POWTREND_RATEDU:
                    prop.SetValue(ratedU);
                    break;

                case ModelCode.POWTREND_CONNKIND:
                    prop.SetValue((short)connectionKind);
                    break;

                case ModelCode.POWTREND_PHASEANGLCLOCK:
                    prop.SetValue(phaseAngleClock);
                    break;

                case ModelCode.POWTREND_POWTR:
                    prop.SetValue(powerTransformers);
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
                case ModelCode.POWTREND_B:
                    b = property.AsFloat();
                    break;

                case ModelCode.POWTREND_B0:
                    b0 = property.AsFloat();
                    break;

                case ModelCode.POWTREND_G:
                    g = property.AsFloat();
                    break;

                case ModelCode.POWTREND_G0:
                    g0 = property.AsFloat();
                    break;

                case ModelCode.POWTREND_R:
                    r = property.AsFloat();
                    break;

                case ModelCode.POWTREND_R0:
                    r0 = property.AsFloat();
                    break;

                case ModelCode.POWTREND_X:
                    x = property.AsFloat();
                    break;

                case ModelCode.POWTREND_X0:
                    x0 = property.AsFloat();
                    break;

                case ModelCode.POWTREND_RATEDS:
                    ratedS = property.AsFloat();
                    break;

                case ModelCode.POWTREND_RATEDU:
                    ratedU = property.AsFloat();
                    break;

                case ModelCode.POWTREND_CONNKIND:
                    connectionKind = (WindingConnection)property.AsEnum();
                    break;

                case ModelCode.POWTREND_PHASEANGLCLOCK:
                    phaseAngleClock = property.AsInt();
                    break;

                case ModelCode.POWTREND_POWTR:
                    powerTransformers = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation
        

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (powerTransformers != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.POWTREND_POWTR] = new List<long>();
                references[ModelCode.POWTREND_POWTR].Add(powerTransformers);
            }

            base.GetReferences(references, refType);
        }
        

        #endregion IReference implementation
    }
}
