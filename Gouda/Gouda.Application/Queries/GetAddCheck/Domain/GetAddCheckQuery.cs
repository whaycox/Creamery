using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Gouda.Application.Queries.GetAddCheck.Domain
{
    using ViewModels.Satellite.Domain;

    public class GetAddCheckQuery : IRequest<NewCheckViewModel>
    {
    }
}
