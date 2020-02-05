using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;

    internal class ModelMapFactory : IModelMapFactory
    {
        private object Locker { get; } = new object();
        private ConcurrentDictionary<Type, ModelMap> BuiltMaps { get; } = new ConcurrentDictionary<Type, ModelMap>();

        private IModelBuilder ModelBuilder { get; }

        public ModelMapFactory(IModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

        public IModelMap<TModel> Build<TModel>()
            where TModel : IDataModel
        {
            if (!BuiltMaps.ContainsKey(typeof(TModel)))
                AddNewModel<TModel>();

            return BuiltMaps[typeof(TModel)] as IModelMap<TModel>;
        }
        private void AddNewModel<TModel>()
            where TModel : IDataModel
        {
            lock (Locker)
            {
                if (!BuiltMaps.ContainsKey(typeof(TModel)))
                {
                    ModelMap modelMap = new ModelMap<TModel>(ModelBuilder);
                    BuiltMaps
                        .TryAddOrFail(typeof(TModel), modelMap)
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }
    }
}
