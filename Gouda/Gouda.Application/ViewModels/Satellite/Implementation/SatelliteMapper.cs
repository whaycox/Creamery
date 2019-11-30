namespace Gouda.Application.ViewModels.Satellite.Implementation
{
    using Abstraction;
    using Domain;
    using Gouda.Domain;

    public class SatelliteMapper : ISatelliteMapper
    {
        public SatelliteViewModel Map(Satellite entity)
        {
            SatelliteViewModel viewModel = new SatelliteViewModel { ID = entity.ID };
            viewModel.NameField.Value = entity.Name;
            viewModel.IPField.Value = entity.IPAddress.ToString();
            viewModel.StatusField.Value = new SatelliteStatusViewModel { Status = entity.Status };

            return viewModel;
        }
    }
}
