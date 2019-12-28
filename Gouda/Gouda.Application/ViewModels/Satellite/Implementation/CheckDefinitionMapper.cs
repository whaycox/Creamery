namespace Gouda.Application.ViewModels.Satellite.Implementation
{
    using Abstraction;
    using Domain;
    using Gouda.Domain;

    public class CheckDefinitionMapper : ICheckDefinitionMapper
    {
        public CheckViewModel Map(CheckDefinition entity) => new CheckViewModel
        {
            Name = entity.Name,
            CheckID = entity.CheckID,
        };
    }
}
