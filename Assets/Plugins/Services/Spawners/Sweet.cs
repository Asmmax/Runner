using Core.Game;
using Leopotam.Ecs;
using Core.Game.Components;
using Core;

namespace Services.Spawners
{
    public class Sweet : ViewableResource
    {
        private int points;

        public int Points
        {
            set { points = value; }
        }

        public Sweet() { }

        protected Sweet(Sweet original) : base(original) {
            points = original.points;
        }

        protected override void ConvertToEntityImpl(IEntityManger entityManager)
        {
            EcsEntity e = entityManager.CreateEntity();
            ref var pos = ref e.Get<Position>();
            ref var bounds = ref e.Get<Bounds>();
            ref var price = ref e.Get<Price>();
            ref var view = ref e.Get<ViewGroup>();

            bounds.height = Size.x;
            bounds.width = Size.y;

            float2 newPos = new float2 { x = Distance, y = TargetField.GetHorizontalPosFor(Line) };
            pos.value.x = newPos.x;
            pos.value.y = newPos.y;

            price.points = points;
            view.id = GetViewGroupID();
        }

        public override ConvertableResource Clone()
        {
            return new Sweet(this);
        }
    }
}

