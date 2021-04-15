using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems
{
    sealed class CharacterMovement : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Position, Speed, Bounds, VerticalTarget, Field> _characters = null;
        ITimeController _time = null;

        void IEcsRunSystem.Run () {

            float deltaTime = _time.DeltaTime;

            int entCount = _characters.GetEntitiesCount();

            for(int id = 0; id < entCount; id++)
            {
                ref var field = ref _characters.Get5(id);
                ref var pos = ref _characters.Get1(id);
                ref var bounds = ref _characters.Get3(id);
                float speed = _characters.Get2(id).value;
                float vertical = _characters.Get4(id).value * (field.extent.y - bounds.height/2);

                float step = speed * deltaTime;

                float dir = vertical - pos.value.y;
                float absDir = (dir > 0 ? dir : -dir);

                step = absDir < step ? absDir : step; //определяем минимальный шаг
                step *= (dir < 0 ? -1 : 1); // добавляем направелние

                pos.value.x += speed * deltaTime;
                pos.value.y += step;
            }
            
        }
    }
}