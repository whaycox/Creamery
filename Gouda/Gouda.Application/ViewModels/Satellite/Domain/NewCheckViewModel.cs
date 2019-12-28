using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Input.Domain;
    using DeferredValues.Domain;
    using Abstraction;
    using Commands.AddCheck.Domain;
    using Input.Abstraction;

    public class NewCheckViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(NewCheckViewModel);

        public CheckDetailsViewModel BasicDetails { get; } = new CheckDetailsViewModel
        {
            Options = new List<IFormInput>
            {
                CheckNameInput,
                RegisteredChecksDropdown,
                RescheduleIntervalInput,
            }
        };

        public ButtonViewModel CancelButton { get; } = new ButtonViewModel
        {
            Label = LabelDeferredKey.CancelButton,
            Editable = true,
        };
        public ButtonViewModel AddButton { get; } = new ButtonViewModel
        {
            Label = LabelDeferredKey.AddButton,
            Editable = true,
            Destination = DestinationDeferredKey.AddCheck,
        };

        private static TextInputViewModel CheckNameInput => new TextInputViewModel
        {
            Label = LabelDeferredKey.CheckName,
            Required = true,
            Name = nameof(AddCheckCommand.Name),
        };
        private static DropdownViewModel RegisteredChecksDropdown => new DropdownViewModel
        {
            Label = LabelDeferredKey.Check,
            Required = true,
            Name = nameof(AddCheckCommand.CheckID),
        };
        private static LongInputViewModel RescheduleIntervalInput => new LongInputViewModel
        {
            Label = LabelDeferredKey.RescheduleInterval,
            Name = nameof(AddCheckCommand.RescheduleInterval),
        };
    }
}
