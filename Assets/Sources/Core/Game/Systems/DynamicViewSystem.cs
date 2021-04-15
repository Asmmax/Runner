using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems
{
    sealed class DynamicViewSystem : IEcsRunSystem
    {
        // auto-injected fields.
        EcsFilter<Position, ViewGroup, DynamicTag> _dynamics = null;
        IImageView _view = null;

        void IEcsRunSystem.Run()
        {
            int entCount = _dynamics.GetEntitiesCount();
            float2[] posData = new float2[entCount];
            int[] eventIDs = new int[entCount];

            for (int id = 0; id < entCount; id++)
            {
                ref var e = ref _dynamics.GetEntity(id);
                eventIDs[id] = e.GetInternalId();
                posData[id] = _dynamics.Get1(id).value;
            }

            _view.UpdatePosition(eventIDs, posData);

        }
    }
}