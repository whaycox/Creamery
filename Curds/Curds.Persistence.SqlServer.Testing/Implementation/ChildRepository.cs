using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

    public class ChildRepository : SimpleSqlRepository<ITestDataModel, Child>, IChildRepository
    {
        public ChildRepository(ISqlQueryBuilder<ITestDataModel> queryBuilder)
            : base(queryBuilder)
        { }

        public Task<List<Child>> ChildrenByParent(int parentID) => FetchEntities(ChildrenByParentQuery(parentID));
        private ISqlQuery<Child> ChildrenByParentQuery(int parentID) => QueryBuilder
            .From<Parent>()
            .Where(parent => parent.ID == parentID)
            .Join(model => model.Children)
                .On(JoinChildren)
                .Inner()
            .Project(ProjectChild);
        private Expression<Func<Parent, Child, bool>> JoinChildren => (parent, child) => parent.ID == child.ParentID;
        private Expression<Func<Parent, Child, Child>> ProjectChild => (parent, child) => child;
    }
}
