using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class ScoreSystem : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Price, CollectedBy> _resources = null;

        void IEcsRunSystem.Run () {
            int resCount = _resources.GetEntitiesCount();

            for(int id = 0; id < resCount; id++)
            {
                EcsEntity character = _resources.Get2(id).character;
                if (!character.Has<Score>()) continue;

                ref var score = ref character.Get<Score>();
                ref var price = ref _resources.Get1(id);
                score.points += price.points;
                price.points = 0;
            }
        }
    }
}