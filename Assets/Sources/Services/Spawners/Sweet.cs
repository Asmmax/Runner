using Core.Game;
using Core.Level;
using Leopotam.Ecs;
using Core.Game.Components;
using Core;

namespace Services.Spawners
{
    public class Sweet : ProceduralResource
    {
        public Sweet(IViewGroupMapper groupMapper) : base(groupMapper) { }

        protected Sweet(Sweet original) : base(original) { }

        protected override void ConvertToEntityFromClone(IEntityManger entityManager)
        {
            EcsEntity e = entityManager.CreateEntity();
            ref var pos = ref e.Get<Position>();
            ref var bounds = ref e.Get<Bounds>();
            ref var price = ref e.Get<Price>();
            ref var view = ref e.Get<ViewGroup>();

            bounds.height = Size.x;
            bounds.width = Size.y;

            float2 newPos = new float2 { x = Distance, y = Field.GetHorizontalPosFor(Line) };
            pos.value.x = newPos.x;
            pos.value.y = newPos.y;

            price.points = 1;
            view.id = ViewGroupMapper.GetID("good");
        }

        protected override ProceduralResource Clone()
        {
            return new Sweet(this);
        }
    }
}

