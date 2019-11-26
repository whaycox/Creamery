namespace Gouda.Application.ViewModels.Inputs.Domain
{
    using Destinations.Abstraction;

    public class FormViewModel : BaseInputCollection
    {
        public override string ViewName => nameof(FormViewModel);

        public string Title { get; set; }
        public IDestinationViewModel Destination { get; set; }
    }
}
