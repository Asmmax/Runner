﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using Interactors;

public class GameModelBinder: System.IDisposable
{
    private GameModel targetModel;
    public GameModelBinder(
        IImageView imageView,
        IList<IScoreView> scoreViews, 
        IHealthView healthView, 
        ITimeController timeController, 
        IInputController inputController, 
        PlayInteractor playInteractor)
    {
        ScoreViewContainer scoreContainer = new ScoreViewContainer(scoreViews);
        targetModel = new UnityIntegratedGameModel(inputController, timeController, imageView, healthView, scoreContainer);
        playInteractor.TargetModel = targetModel;
    }

    public void Dispose()
    {
        targetModel.Dispose();
    }
}