using Core.Level;
using Core.Game;

namespace Services.Spawners
{
    public abstract class ConvertableResource : Resource, IEntityConverter
    {
        private bool isSpawned = false;
        private Field targetField;

        public Field TargetField { get { return targetField; } }

        public void ConvertToEntity(IEntityManger entityManager)
        {
            if (!isSpawned) return;
            ConvertToEntityImpl(entityManager);
        }

        public ConvertableResource() { }
        public ConvertableResource(ConvertableResource original) : base(original) { }

        public override void Spawn(Field field)
        {
            isSpawned = true;
            targetField = field;
        }

        protected abstract void ConvertToEntityImpl(IEntityManger entityManager);

        public abstract ConvertableResource Clone();
    }
}
