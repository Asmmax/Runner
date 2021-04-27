using Core.Game;
using Leopotam.Ecs;
using Core.Game.Components;
using Core;

namespace Services.Spawners
{
    public class Life : ViewableResource
    {
        private uint count;

        public uint Count
        {
            set { count = value; }
        }

        public Life() { }

        protected Life(Life original) : base(original) {
            count = original.count;
        }

        protected override void ConvertToEntityImpl(IEntityManger entityManager)
        {
            EcsEntity e = entityManager.CreateEntity();
            ref var pos = ref e.Get<Position>();
            ref var bounds = ref e.Get<Bounds>();
            ref var repair = ref e.Get<Damage>();
            ref var view = ref e.Get<ViewGroup>();

            bounds.height = Size.x;
            bounds.width = Size.y;

            float2 newPos = new float2 { x = Distance, y = TargetField.GetHorizontalPosFor(Line) };
            pos.value.x = newPos.x;
            pos.value.y = newPos.y;

            repair.points = -(int)count;
            view.id = GetViewGroupID();
        }

        public override ConvertableResource Clone()
        {
            return new Life(this);
        }
    }
}

