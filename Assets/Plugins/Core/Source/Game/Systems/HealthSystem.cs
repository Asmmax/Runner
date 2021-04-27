using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class HealthSystem : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Damage, CollectedBy> _resources = null;

        void IEcsRunSystem.Run()
        {
            int resCount = _resources.GetEntitiesCount();

            for (int id = 0; id < resCount; id++)
            {
                EcsEntity character = _resources.Get2(id).character;
                if (!character.Has<Health>()) continue;

                ref var health = ref character.Get<Health>();
                ref var damage = ref _resources.Get1(id);
                health.points -= damage.points;
                damage.points = 0;
            }
        }
    }
}