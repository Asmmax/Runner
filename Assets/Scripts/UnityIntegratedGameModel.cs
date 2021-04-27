using System.Collections.Generic;
using Core.Game;

public class UnityIntegratedGameModel : GameModel
{

    public UnityIntegratedGameModel(IInputController inputController,
                        ITimeController timeController,
                        IImageView imageView,
                        IHealthView healthView,
                        IScoreView scoreView) :
        base(inputController, timeController, imageView, healthView, scoreView)
    {
    }

    protected override void InitializeBeforeInitSystem()
    {
        
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
        
    }
}
