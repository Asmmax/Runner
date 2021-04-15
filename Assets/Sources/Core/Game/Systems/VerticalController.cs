using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems {
    sealed class VerticalController : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<VerticalTarget, PlayerTag> _controllers = null;
        IInputController _input = null;

        float oldVertical = 0;

        void IEcsRunSystem.Run () {

            float newVertical = _input.Vertical;

            if (newVertical != oldVertical)
            {

                int entCount = _controllers.GetEntitiesCount();
                for (int id = 0; id < entCount; id++)
                {
                    ref var vertical = ref _controllers.Get1(id);
                    vertical.value = newVertical;
                }

                oldVertical = newVertical;
            }
        }
    }
}