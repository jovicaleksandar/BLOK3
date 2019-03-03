using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class ConnectivityNode : IdentifiedObject
    {
        private string description;

        private List<long> terminals = new List<long>();

        public ConnectivityNode(long globalId) : base(globalId) 
		{
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public List<long> Terminals
        {
            get
            {
                return terminals;
            }

            set
            {
                terminals = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ConnectivityNode x = (ConnectivityNode)obj;
                return ((x.Description == this.Description) &&
                        (CompareHelper.CompareLists(x.terminals, this.terminals)));
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
                case ModelCode.CONNECTIVITYNODE_DESC:
                case ModelCode.CONNECTIVITYNODE_TERMINALS:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CONNECTIVITYNODE_DESC:
                    prop.SetValue(description);
                    break;
                case ModelCode.CONNECTIVITYNODE_TERMINALS:
                    prop.SetValue(terminals);
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
                case ModelCode.CONNECTIVITYNODE_DESC:
                    description = property.AsString();
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
                return terminals.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (terminals != null && terminals.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONNECTIVITYNODE_TERMINALS] = terminals.GetRange(0, terminals.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TERMINAL_CONNECTIVITYNODE:
                    terminals.Add(globalId);
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
                case ModelCode.TERMINAL_CONNECTIVITYNODE:

                    if (terminals.Contains(globalId))
                    {
                        terminals.Remove(globalId);
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

