using Leopotam.Ecs;
using Core.Game.Components;

namespace Core.Game.Systems
{
    sealed class ScoreView : IEcsRunSystem {
        // auto-injected fields.
        EcsFilter<Score, PlayerTag> _scores = null;
        IScoreView _view = null;

        void IEcsRunSystem.Run () {
            int entCount = _scores.GetEntitiesCount();
            if (entCount == 0) return;

            ref var score = ref _scores.Get1(0);

            _view.Update(score.points);
        }
    }
}