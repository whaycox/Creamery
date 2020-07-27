using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    public interface IAssignIdentityExpressionBuilder
    {
        AssignIdentityDelegate BuildAssignIdentityDelegate(IEntityModel entityModel);
    }
}
