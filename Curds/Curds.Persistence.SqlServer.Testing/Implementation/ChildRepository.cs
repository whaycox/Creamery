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

        private ISqlUniverse<ITestDataModel, Parent> ParentByIDUniverse(int parentID) => QueryBuilder
            .From<Parent>()
            .Where(parent => parent.ID == parentID);
        private ISqlUniverse<ITestDataModel, Parent, Child> ChildrenByParentIDUniverse(int parentID) => ParentByIDUniverse(parentID)
            .Join(model => model.Children)
                .On(JoinChildren)
                .Inner();
        private Expression<Func<Parent, Child, bool>> JoinChildren => (parent, child) => parent.ID == child.ParentID;
        private Expression<Func<Parent, Child, bool>> FilterEvenChildren => (parent, child) => child.ID % 2 == 0;
        private Expression<Func<Parent, Child, bool>> FilterOddChildren => (parent, child) => child.ID % 2 != 0;
        private Expression<Func<Parent, Child, Child>> ProjectChild => (parent, child) => child;

        public Task<List<Child>> ChildrenByParent(int parentID) => FetchEntities(ChildrenByParentQuery(parentID));
        private ISqlQuery<Child> ChildrenByParentQuery(int parentID) => ChildrenByParentIDUniverse(parentID)
            .Project(ProjectChild);

        public Task<List<Child>> EvenChildrenByParent(int parentID) => FetchEntities(EvenChildrenByParentQuery(parentID));
        private ISqlQuery<Child> EvenChildrenByParentQuery(int parentID) => ChildrenByParentIDUniverse(parentID)
            .Where(FilterEvenChildren)
            .Project(ProjectChild);

        public Task<List<Child>> OddChildrenByParent(int parentID) => FetchEntities(OddChildrenByParentQuery(parentID));
        private ISqlQuery<Child> OddChildrenByParentQuery(int parentID) => ParentByIDUniverse(parentID)
            .Join(model => model.Children)
                .On(JoinChildren)
                .On(FilterOddChildren)
                .Inner()
            .Project(ProjectChild);

    }
}
