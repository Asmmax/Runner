using Core.Game;
using Core.Level;
using System.Collections.Generic;

namespace Services.Spawners
{
    public abstract class ProceduralResource : Resource, IEntityConverter
    {
        private IList<ProceduralResource> clones = new List<ProceduralResource>();

        private IViewGroupMapper groupMapper;

        protected IViewGroupMapper ViewGroupMapper{
            get{ return groupMapper; }
        }

        public ProceduralResource(IViewGroupMapper groupMapper)
        {
            this.groupMapper = groupMapper;
        }

        protected ProceduralResource(ProceduralResource original) : base(original)
        {
            groupMapper = original.groupMapper;
        }

        public void ConvertToEntity(IEntityManger entityManager)
        {
            foreach (var clone in clones)
            {
                clone.ConvertToEntityFromClone(entityManager);
            }
        }

        public override void Spawn()
        {
            clones.Add(Clone());
        }

        abstract protected void ConvertToEntityFromClone(IEntityManger entityManager);

        abstract protected ProceduralResource Clone();

    }
}
