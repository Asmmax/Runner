using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems
{
    sealed class FollowerMovement : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Position, Target> _followers = null;

        void IEcsRunSystem.Run () {
            int entCount = _followers.GetEntitiesCount();

            for (int id = 0; id < entCount; id++)
            {
                EcsEntity target = _followers.Get2(id).value;
                ref var pos = ref _followers.Get1(id);

                if (!target.Has<Position>() || !target.Has<Field>()) continue;

                float2 targetPos = target.Get<Position>().value;
                float2 fieldCenter = target.Get<Field>().pivot;

                pos.value.x = targetPos.x;
                pos.value.y = fieldCenter.y;
            }
        }
    }
}