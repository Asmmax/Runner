using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;

public class GameModelBinder: System.IDisposable
{
    private GameModel targetModel;
    public GameModelBinder(
        IImageView imageView,
        IList<IScoreView> scoreViews, 
        IHealthView healthView, 
        ITimeController timeController, 
        IInputController inputController, 
        GameController gameController)
    {
        ScoreViewContainer scoreContainer = new ScoreViewContainer(scoreViews);
        targetModel = new UnityIntegratedGameModel(inputController, timeController, imageView, healthView, scoreContainer);
        gameController.TargetModel = targetModel;
    }

    public void Dispose()
    {
        targetModel.Dispose();
    }
}
