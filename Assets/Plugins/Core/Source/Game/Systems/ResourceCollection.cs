using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems
{
    sealed class ResourceCollection : IEcsRunSystem
    {
        // auto-injected fields.
        EcsFilter<Position, Bounds>.Exclude<Speed, CollectedBy> _resources = null;
        EcsFilter<Position, Bounds, Speed> _characters = null;
        void IEcsRunSystem.Run()
        {
            int resCount = _resources.GetEntitiesCount();
            int charCount = _characters.GetEntitiesCount();

            //за один кадр можно собрать только одну еденицу ресурса
            EcsEntity collectedEntity = EcsEntity.Null;
            EcsEntity collector = EcsEntity.Null;

            for (int res_id = 0; res_id < resCount; res_id++)
            {
                ref var resPos = ref _resources.Get1(res_id);
                ref var resSize = ref _resources.Get2(res_id);

                for (int char_id = 0; char_id < charCount; char_id++)
                {
                    ref var charPos = ref _characters.Get1(char_id);
                    ref var charSize = ref _characters.Get2(char_id);
                    
                    float distancex = (charPos.value.x - resPos.value.x);
                    float distancey = (charPos.value.y - resPos.value.y);

                    distancex = distancex<0 ? -distancex : distancex;
                    distancey = distancey < 0 ? -distancey : distancey;

                    if (distancex * 2 < charSize.width + resSize.width
                        && distancey * 2 < charSize.height + resSize.height)
                    {
                        collectedEntity = _resources.GetEntity(res_id);
                        collector = _characters.GetEntity(char_id);
                    }
                }
            }

            if (!collectedEntity.IsNull() && !collector.IsNull())
            {
                ref var collectedBy = ref collectedEntity.Get<CollectedBy>();
                collectedBy.character = collector;
            }
        }
    }
}