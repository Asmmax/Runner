using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class LoseSystem : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Health, PlayerTag> _healthes = null;
        IGameController _game = null;

        void IEcsRunSystem.Run () {

            if (_healthes.GetEntitiesCount() == 0) return;

            if (_healthes.Get1(0).points <= 0)
            {
                _game.Lose();
            }

        }
    }
}