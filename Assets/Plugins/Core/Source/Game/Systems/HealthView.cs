using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class HealthView : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Health, PlayerTag> _healthes = null;
        IHealthView _view = null;

        void IEcsRunSystem.Run()
        {
            if (_healthes.GetEntitiesCount() == 0) return;

            ref var health = ref _healthes.Get1(0);

            _view.Update(health.points);
        }
    }
}