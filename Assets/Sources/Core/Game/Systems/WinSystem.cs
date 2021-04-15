using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class WinSystem : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Position, Bounds, Field, PlayerTag> _characters = null;
        IGameController _game = null;

        void IEcsRunSystem.Run()
        {

            if (_characters.GetEntitiesCount() == 0) return;

            ref var pos = ref _characters.Get1(0);
            ref var bounds = ref _characters.Get2(0);
            ref var field = ref _characters.Get3(0);

            if (pos.value.x - bounds.width/2 > field.pivot.x + field.extent.x)
            {
                _game.Win();
            }

        }
    }
}