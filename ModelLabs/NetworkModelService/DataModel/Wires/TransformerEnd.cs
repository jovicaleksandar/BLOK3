using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TransformerEnd : IdentifiedObject
    {
        private long ratioTapChanger = 0;
        private long terminal = 0;

        public TransformerEnd(long globalId) : base(globalId) 
		{

        }

        public long RatioTapChanger
        {
            get
            {
                return ratioTapChanger;
            }

            set
            {
                ratioTapChanger = value;
            }
        }

        public long Terminal
        {
            get
            {
                return terminal;
            }

            set
            {
                terminal = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TransformerEnd x = (TransformerEnd)obj;
                return (x.ratioTapChanger == this.ratioTapChanger &&
                        x.terminal == this.terminal );
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
                case ModelCode.TRANSFORMEREND_RATIOTAPCHNG:
                case ModelCode.TRANSFORMEREND_TERMINAL:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TRANSFORMEREND_RATIOTAPCHNG:
                    property.SetValue(ratioTapChanger);
                    break;

                case ModelCode.TRANSFORMEREND_TERMINAL:
                    property.SetValue(terminal);
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
                case ModelCode.TRANSFORMEREND_RATIOTAPCHNG:
                    ratioTapChanger = property.AsReference();
                    break;

                case ModelCode.TRANSFORMEREND_TERMINAL:
                    terminal = property.AsReference();
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
            if (ratioTapChanger != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.TRANSFORMEREND_RATIOTAPCHNG] = new List<long>();
                references[ModelCode.TRANSFORMEREND_RATIOTAPCHNG].Add(ratioTapChanger);
            }

            if (terminal != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.TRANSFORMEREND_TERMINAL] = new List<long>();
                references[ModelCode.TRANSFORMEREND_TERMINAL].Add(terminal);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
