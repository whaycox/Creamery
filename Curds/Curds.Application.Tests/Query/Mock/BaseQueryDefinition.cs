using System;

namespace Curds.Application.Query.Mock
{
    using Application.Mock;
    using System.Threading.Tasks;

    public class BaseQueryDefinition : Domain.BaseQueryDefinition<CurdsApplication, BaseQuery, BaseViewModel>
    {
        public BaseQueryDefinition(CurdsApplication mockApplication)
            : base(mockApplication)
        { }

        public override Task<BaseViewModel> Execute(BaseQuery message)
        {
            throw new NotImplementedException();
        }
    }
}
