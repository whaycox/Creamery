using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Gouda.Application.Queries.GetAddCheck.Implementation
{
    using Domain;
    using ViewModels.Satellite.Domain;
    using Checks.Abstraction;
    using Gouda.Abstraction;
    using ViewModels.Input.Domain;

    public class GetAddCheckHandler : IRequestHandler<GetAddCheckQuery, NewCheckViewModel>
    {
        private ICheckLibrary CheckLibrary { get; }

        public GetAddCheckHandler(ICheckLibrary checkLibrary)
        {
            CheckLibrary = checkLibrary;
        }

        public Task<NewCheckViewModel> Handle(GetAddCheckQuery request, CancellationToken cancellationToken) => Task.FromResult(HandleInternal(request));
        private NewCheckViewModel HandleInternal(GetAddCheckQuery query)
        {
            NewCheckViewModel viewModel = new NewCheckViewModel();
            DropdownViewModel registeredChecks = viewModel.BasicDetails.Options.First(option => option is DropdownViewModel) as DropdownViewModel;

            registeredChecks.Items.Add(new DropdownItemViewModel());
            foreach (ICheck registeredCheck in CheckLibrary.RegisteredChecks)
                AddCheckToViewModel(registeredCheck, registeredChecks);
            return viewModel;
        }
        private void AddCheckToViewModel(ICheck registeredCheck, DropdownViewModel viewModel) => viewModel
            .Items
            .Add(BuildCheckItemViewModel(registeredCheck));
        private DropdownItemViewModel BuildCheckItemViewModel(ICheck check) => new DropdownItemViewModel
        {
            Value = check.ID.ToString(),
            Description = check.Name
        };
    }
}
