using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class ResourceUtilization : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<CollectedBy> _collected = null;

        void IEcsRunSystem.Run () {
            foreach (var idx in _collected)
            {
                _collected.GetEntity(idx).Destroy();
            }
        }
    }
}