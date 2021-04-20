using Core.Game;
using Core.Level;
using System.Collections.Generic;

namespace Services.Spawners
{
    public class SpawningResource : ConvertableResource
    {
        private IList<ConvertableResource> clones = new List<ConvertableResource>();

        private ConvertableResource clonable;

        public SpawningResource(SpawningResource original): base(original)
        {
            clonable = original.clonable;
            clones = new List<ConvertableResource>(original.clones);
        }

        public SpawningResource(ConvertableResource clonable)
        {
            this.clonable = clonable;
            Size = clonable.Size;
        }

        public override ConvertableResource Clone()
        {
            return new SpawningResource(this);
        }

        public override void Spawn(Field field)
        {
            ConvertableResource convertableResource = clonable.Clone();
            convertableResource.Distance = Distance;
            convertableResource.Line = Line;
            convertableResource.Spawn(field);
            clones.Add(convertableResource);
            base.Spawn(field);
        }

        protected override void ConvertToEntityImpl(IEntityManger entityManager)
        {
            foreach (var clone in clones)
            {
                clone.ConvertToEntity(entityManager);
            }
        }
    }
}
