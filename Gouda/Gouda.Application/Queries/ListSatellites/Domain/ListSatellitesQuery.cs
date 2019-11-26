using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Gouda.Application.Queries.ListSatellites.Domain
{
    public class ListSatellitesQuery : IRequest<ListSatellitesResult>
    {
    }
}
