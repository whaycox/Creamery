namespace Gouda.Application.ViewModels.Input.Domain
{
    public class DropdownItemViewModel : BaseInputViewModel
    {
        public override string ViewName => nameof(DropdownItemViewModel);

        public string Value { get; set; }
        public string Description { get; set; }
    }
}
