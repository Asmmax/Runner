using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class HideResourceSystem : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<CollectedBy> _collected = null;
        IImageView _view = null;

        public void Run()
        {
            int colCount = _collected.GetEntitiesCount();
            if (colCount == 0) return;

            int[] ids = new int[colCount];

            for (int id = 0; id < colCount; id++)
            {
                ids[id] = _collected.GetEntity(id).GetInternalId();
            }

            _view.Hide(ids);
        }
    }
}