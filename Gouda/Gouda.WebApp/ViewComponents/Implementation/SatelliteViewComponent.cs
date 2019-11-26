namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Gouda.Domain;
    using Application.ViewModels.Satellite.Abstraction;

    public class SatelliteViewComponent : BaseViewModelViewComponent<ISatelliteViewModel>
    {
        public const string Name = nameof(Satellite);
    }
}
