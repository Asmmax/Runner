using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class ViewSystem : IEcsInitSystem {
        // auto-injected fields.
        EcsFilter<Position, ViewGroup> _views = null;
        IImageView _view = null;

        void IEcsInitSystem.Init()
        {
            int resCount = _views.GetEntitiesCount();
            float2[] posData = new float2[resCount];
            int[] groupData = new int[resCount];
            int[] ids = new int[resCount];

            for (int id = 0; id < resCount; id++)
            {
                ref var e = ref _views.GetEntity(id);
                ids[id] = e.GetInternalId();
                posData[id] = _views.Get1(id).value;
                groupData[id] = _views.Get2(id).id;
            }

            _view.Show(ids, groupData);
            _view.UpdatePosition(ids, posData);
        }
    }
}