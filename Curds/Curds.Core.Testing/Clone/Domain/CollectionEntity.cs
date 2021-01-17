using System.Collections.Generic;

namespace Curds.Clone.Domain
{
    public class CollectionEntity
    {
        public int[] IntArray { get; set; }
        public PrimitiveEntity[] PrimitiveEntityArray { get; set; }
        public ComplexEntity[] ComplexEntityArray { get; set; }

        public List<long> LongList { get; set; }
        public List<PrimitiveEntity> PrimitiveEntityList { get; set; }
        public List<ComplexEntity> ComplexEntityList { get; set; }

        public CollectionEntity()
        { }
        public CollectionEntity(int entities)
        {
            IntArray = BuildIntArray(entities);
            PrimitiveEntityArray = BuildPrimitiveEntityArray(entities);
            ComplexEntityArray = BuildComplexEntityArray(entities);

            LongList = BuildLongList(entities);
            PrimitiveEntityList = BuildPrimitiveEntityList(entities);
            ComplexEntityList = BuildComplexEntityList(entities);
        }

        private int[] BuildIntArray(int entities)
        {
            int[] array = new int[entities];
            for (int i = 0; i < entities; i++)
                array[i] = i;
            return array;
        }
        private PrimitiveEntity[] BuildPrimitiveEntityArray(int entities)
        {
            PrimitiveEntity[] array = new PrimitiveEntity[entities];
            for (int i = 0; i < entities; i++)
                array[i] = new PrimitiveEntity();
            return array;
        }
        private ComplexEntity[] BuildComplexEntityArray(int entities)
        {
            ComplexEntity[] array = new ComplexEntity[entities];
            for (int i = 0; i < entities; i++)
                array[i] = new ComplexEntity { TestPrimitiveEntity = PrimitiveEntityArray[i] };
            return array;
        }

        private List<long> BuildLongList(int entities)
        {
            List<long> list = new List<long>(entities);
            for (int i = 0; i < entities; i++)
                list.Add(i);
            return list;
        }
        private List<PrimitiveEntity> BuildPrimitiveEntityList(int entities)
        {
            List<PrimitiveEntity> list = new List<PrimitiveEntity>(entities);
            for (int i = 0; i < entities; i++)
                list.Add(new PrimitiveEntity());
            return list;
        }
        private List<ComplexEntity> BuildComplexEntityList(int entities)
        {
            List<ComplexEntity> list = new List<ComplexEntity>(entities);
            for (int i = 0; i < entities; i++)
                list.Add(new ComplexEntity { TestPrimitiveEntity = PrimitiveEntityList[i] });
            return list;
        }
    }
}
