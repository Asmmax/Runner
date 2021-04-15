using Leopotam.Ecs;
using System.Collections.Generic;

namespace Core.Game.Systems
{
    sealed class ConversionSystem : IEcsInitSystem {
        EcsWorld _world = null;
        IEntityConverter _converter = null;

        public void Init () {
            EcsEntityManager entityManager = new EcsEntityManager(_world);
            if (_converter != null) _converter.ConvertToEntity(entityManager);
        }
    }
}