﻿using System.Collections.Generic;

namespace Gouda.Application.DeferredValues.Implementation
{
    using Abstraction;
    using DeferredValues.Domain;

    public class LabelDeferredValue : ILabelDeferredValue
    {
        private Dictionary<LabelDeferredKey, string> Labels = new Dictionary<LabelDeferredKey, string>();

        public string this[LabelDeferredKey key] => Labels[key];

        public LabelDeferredValue()
        {
            RegisterValues();
        }
        private void RegisterValues()
        {
            RegisterButtons();
            RegisterSatellite();
        }
        private void RegisterButtons()
        {
            Labels.Add(LabelDeferredKey.AddButton, "Add");
            Labels.Add(LabelDeferredKey.CancelButton, "Cancel");
            Labels.Add(LabelDeferredKey.DeleteButton, "Delete");
        }
        private void RegisterSatellite()
        {
            Labels.Add(LabelDeferredKey.Satellite, "Satellite");
            Labels.Add(LabelDeferredKey.Satellites, "Satellites");
            Labels.Add(LabelDeferredKey.SatelliteIP, "Satellite IP");
            Labels.Add(LabelDeferredKey.SatelliteName, "Satellite Name");
            Labels.Add(LabelDeferredKey.SatelliteStatus, "Satellite Status");
            Labels.Add(LabelDeferredKey.AddSatellite, "Add a Satellite");
            Labels.Add(LabelDeferredKey.Check, "Check");
            Labels.Add(LabelDeferredKey.Checks, "Checks");
            Labels.Add(LabelDeferredKey.CheckName, "Check Name");
            Labels.Add(LabelDeferredKey.RescheduleInterval, "Reschedule Interval (mins)");
        }
    }
}
