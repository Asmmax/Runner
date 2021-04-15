using Core.Game;
using Core.Level;
using Leopotam.Ecs;
using Core.Game.Components;
using Core;
using System.Collections.Generic;

namespace Services.Spawners
{
    public class Life : ProceduralResource
    {
        public Life(IViewGroupMapper groupMapper):base(groupMapper) { }

        protected Life(Life original) : base(original) { }

        protected override void ConvertToEntityFromClone(IEntityManger entityManager)
        {
            EcsEntity e = entityManager.CreateEntity();
            ref var pos = ref e.Get<Position>();
            ref var bounds = ref e.Get<Bounds>();
            ref var repair = ref e.Get<Damage>();
            ref var view = ref e.Get<ViewGroup>();

            bounds.height = Size.x;
            bounds.width = Size.y;

            float2 newPos = new float2 { x = Distance, y = Field.GetHorizontalPosFor(Line) };
            pos.value.x = newPos.x;
            pos.value.y = newPos.y;

            repair.points = -1;
            view.id = ViewGroupMapper.GetID("life");
        }

        protected override ProceduralResource Clone()
        {
            return new Life(this);
        }
    }
}

