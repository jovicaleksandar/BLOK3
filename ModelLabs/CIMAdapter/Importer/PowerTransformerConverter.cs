namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulateConnectivityNodeProperties(ConnectivityNode cimConnectivityNode, ResourceDescription rd)
		{
			if ((cimConnectivityNode != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimConnectivityNode, rd);

				if (cimConnectivityNode.DescriptionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONNECTIVITYNODE_DESC, cimConnectivityNode.Description));
				}
            }
		}

		public static void PopulatePowerSystemResourceProperties(PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		public static void PopulateTerminalProperties(Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTerminal != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);

                if (cimTerminal.ConnectedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTED, cimTerminal.Connected));
                }

                if (cimTerminal.PhasesHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_PHASES, (short)GetDMSPhaseCode(cimTerminal.Phases)));
                }

                if (cimTerminal.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TERMINAL_SEQNUM, cimTerminal.SequenceNumber));
                }

                if (cimTerminal.ConductingEquipmentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONDEQ, gid));
                }

                if (cimTerminal.ConnectivityNodeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTerminal.ConnectivityNode.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTIVITYNODE, gid));
                }
            }
		}

		public static void PopulateEquipmentProperties(Equipment cimEquipment, ResourceDescription rd)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

				if (cimEquipment.AggregateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}
				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(ConductingEquipment cimConductingEquipment, ResourceDescription rd)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
			}
		}

		public static void PopulatePowerTransformerProperties(PowerTransformer cimPowerTransformer, ResourceDescription rd)
		{
			if ((cimPowerTransformer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd);

				if (cimPowerTransformer.VectorGroupHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTR_VECTORGROUP, cimPowerTransformer.VectorGroup));
				}
			}
		}

        public static void PopulateTransformerEndProperties(TransformerEnd cimTransformerEnd, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTransformerEnd != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTransformerEnd, rd);
            }

            if (cimTransformerEnd.RatioTapChangerHasValue)
            {
                long gid = importHelper.GetMappedGID(cimTransformerEnd.RatioTapChanger.ID);
                if (gid < 0)
                {
                    report.Report.Append("WARNING: Convert ").Append(cimTransformerEnd.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerEnd.ID);
                    report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimTransformerEnd.RatioTapChanger.ID).AppendLine(" \" is not mapped to GID!");
                }
                rd.AddProperty(new Property(ModelCode.TRANSFORMEREND_RATIOTAPCHNG, gid));
            }

            if (cimTransformerEnd.TerminalHasValue)
            {
                long gid = importHelper.GetMappedGID(cimTransformerEnd.Terminal.ID);
                if (gid < 0)
                {
                    report.Report.Append("WARNING: Convert ").Append(cimTransformerEnd.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerEnd.ID);
                    report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimTransformerEnd.Terminal.ID).AppendLine(" \" is not mapped to GID!");
                }
                rd.AddProperty(new Property(ModelCode.TRANSFORMEREND_TERMINAL, gid));
            }
        }

        public static void PopulatePowerTransformerEndProperties(PowerTransformerEnd cimPowerTransformerEnd, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPowerTransformerEnd != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateTransformerEndProperties(cimPowerTransformerEnd, rd, importHelper, report);
            }

            if (cimPowerTransformerEnd.PowerTransformerHasValue)
            {
                long gid = importHelper.GetMappedGID(cimPowerTransformerEnd.PowerTransformer.ID);
                if (gid < 0)
                {
                    report.Report.Append("WARNING: Convert ").Append(cimPowerTransformerEnd.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerTransformerEnd.ID);
                    report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimPowerTransformerEnd.RatioTapChanger.ID).AppendLine(" \" is not mapped to GID!");
                }
                rd.AddProperty(new Property(ModelCode.POWTREND_POWTR, gid));
            }

            if (cimPowerTransformerEnd.BHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_B, cimPowerTransformerEnd.B));
            }
            if (cimPowerTransformerEnd.B0HasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_B0, cimPowerTransformerEnd.B0));
            }

            if (cimPowerTransformerEnd.GHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_G, cimPowerTransformerEnd.G));
            }
            if (cimPowerTransformerEnd.G0HasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_G0, cimPowerTransformerEnd.G0));
            }

            if (cimPowerTransformerEnd.RHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_R, cimPowerTransformerEnd.R));
            }
            if (cimPowerTransformerEnd.R0HasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_R0, cimPowerTransformerEnd.R0));
            }

            if (cimPowerTransformerEnd.XHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_X, cimPowerTransformerEnd.X));
            }
            if (cimPowerTransformerEnd.X0HasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_X0, cimPowerTransformerEnd.X0));
            }

            if (cimPowerTransformerEnd.RatedUHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_RATEDU, cimPowerTransformerEnd.RatedU));
            }
            if (cimPowerTransformerEnd.RatedSHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_RATEDS, cimPowerTransformerEnd.RatedS));
            }

            if (cimPowerTransformerEnd.PhaseAngleClockHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_PHASEANGLCLOCK, cimPowerTransformerEnd.PhaseAngleClock));
            }
            if (cimPowerTransformerEnd.ConnectionKindHasValue)
            {
                rd.AddProperty(new Property(ModelCode.POWTREND_CONNKIND, (short)GetDMSWindingConnection(cimPowerTransformerEnd.ConnectionKind)));
            }
        }

        public static void PopulateTapChangerProperties(TapChanger cimTapChanger, ResourceDescription rd)
        {
            if ((cimTapChanger != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimTapChanger, rd);
            }

            if (cimTapChanger.HighStepHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_HIGHSTEP, cimTapChanger.HighStep));
            }

            if (cimTapChanger.InitialDelayHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_INITIALDELAY, cimTapChanger.InitialDelay));
            }

            if (cimTapChanger.LowStepHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_LOWSTEP, cimTapChanger.LowStep));
            }

            if (cimTapChanger.LtcFlagHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_LTCFLAG, cimTapChanger.LtcFlag));
            }

            if (cimTapChanger.NeutralStepHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_NEUTRALSTEP, cimTapChanger.NeutralStep));
            }

            if (cimTapChanger.NeutralUHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_NEUTRALU, cimTapChanger.NeutralU));
            }

            if (cimTapChanger.NormalStepHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_NORMALSTEP, cimTapChanger.NormalStep));
            }

            if (cimTapChanger.RegulationStatusHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_REGULATIONSTATUS, cimTapChanger.RegulationStatus));
            }

            if (cimTapChanger.SubsequentDelayHasValue)
            {
                rd.AddProperty(new Property(ModelCode.TAPCHANGER_SUBSEQUENTDELAY, cimTapChanger.SubsequentDelay));
            }
        }

        public static void PopulateRatioTapChangerProperties(RatioTapChanger cimRatioTapChanger, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRatioTapChanger != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateTapChangerProperties(cimRatioTapChanger, rd);
            }

            if (cimRatioTapChanger.StepVoltageIncrementHasValue)
            {
                rd.AddProperty(new Property(ModelCode.RATIOTAPCHANGER_STEPVOLTAGEINC, cimRatioTapChanger.StepVoltageIncrement));
            }

            if (cimRatioTapChanger.TculControlModeHasValue)
            {
                rd.AddProperty(new Property(ModelCode.RATIOTAPCHANGER_TCULCONTROLMODE, (short)GetTransformerControlMode(cimRatioTapChanger.TculControlMode)));
            }

            //if (cimRatioTapChanger.TransformerEndHasValue)
            //{
            //    long gid = importHelper.GetMappedGID(cimRatioTapChanger.TransformerEnd.ID);
            //    if (gid < 0)
            //    {
            //        report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
            //        report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
            //    }
            //    rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTIVITYNODE, gid));
            //}
        }

        #endregion Populate ResourceDescription

        #region Enums convert
        public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.Unknown;
			}
		}

        //public static TransformerFunction GetDMSTransformerFunctionKind(TransformerFunctionKind transformerFunction)
        //{
        //	switch (transformerFunction)
        //	{
        //		case FTN.TransformerFunctionKind.voltageRegulator:
        //			return TransformerFunction.Voltreg;
        //		default:
        //			return TransformerFunction.Consumer;
        //	}
        //}

        public static FTN.TransformerControlMode GetTransformerControlMode(FTN.TransformerControlMode tcm)
        {
            switch (tcm)
            {
                case FTN.TransformerControlMode.volt:
                    return FTN.TransformerControlMode.volt;
                case FTN.TransformerControlMode.reactive:
                    return FTN.TransformerControlMode.reactive;
                default:
                    return FTN.TransformerControlMode.volt;
            }
        }

        public static WindingType GetDMSWindingType(WindingType windingType)
		{
			switch (windingType)
			{
				case WindingType.Primary:
					return WindingType.Primary;
				case WindingType.Secondary:
					return WindingType.Secondary;
				case WindingType.Tertiary:
					return WindingType.Tertiary;
				default:
					return WindingType.None;
			}
		}

		public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
		{
			switch (windingConnection)
			{
				case FTN.WindingConnection.D:
					return WindingConnection.D;
				case FTN.WindingConnection.I:
					return WindingConnection.I;
				case FTN.WindingConnection.Z:
					return WindingConnection.Z;
				case FTN.WindingConnection.Y:
					return WindingConnection.Y;
				default:
					return WindingConnection.Y;
			}
		}
		#endregion Enums convert
	}
}
