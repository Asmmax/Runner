using Core.Game;
using Core.Data;
using System.Collections.Generic;

namespace Interactors
{
    public interface IConverterGateway
    {
        IEntityConverter GetConverterFor(int level);
    }

    public interface IRenderService
    {
        void FinishByWin(System.Action afterFinished);
        void FinishByLose(System.Action afterFinished);
        void ForceFinish();
    }

    public interface ILevelGateway
    {
        Level GetLevelStats(int level);
        void PutLevelStats(int level, Level stats);
    }

    public class PlayInteractor :  IGameController, IScoreView
    {
        bool isPlayed = false;

        int curLevel = 0;
        int curScore = 0;
        IConverterGateway converterGateway;
        ILevelGateway levelGateway;
        IInputController inputController;

        IRenderService renderService;
        IList<System.Action> loseCallbacks = new List<System.Action>();
        IList<System.Action> winCallbacks = new List<System.Action>();


        GameModel targetModel;

        public GameModel TargetModel
        {
            set
            {
                targetModel = value;
                targetModel.GameController = this;
            }
        }

        public PlayInteractor(IConverterGateway converterGateway, ILevelGateway levelGateway, IRenderService renderService, IInputController inputController)
        {
            this.converterGateway = converterGateway;
            this.levelGateway = levelGateway;
            this.renderService = renderService;
            this.inputController = inputController;

            inputController.Lock();
        }


        public void Play(int level)
        {
            if (isPlayed) return;
            isPlayed = true;

            renderService.ForceFinish();

            inputController.Unlock();

            curLevel = level;
            IEntityConverter entityConverter = converterGateway.GetConverterFor(level);
            targetModel.EntityConverter = entityConverter;

            targetModel.Initialize();
        }

        public void Update()
        {
            if(targetModel != null && targetModel.IsValid())
            {
                targetModel.Update();
            }
        }

        public void Retry()
        {
            Stop();
            Play(curLevel);
        }

        public void Stop()
        {
            if (!isPlayed) return;
            isPlayed = false;

            StopImpl();
            renderService.ForceFinish();
        }

        private void StopImpl()
        {
            targetModel.Dispose();
            inputController.Reset();
            inputController.Lock();
            curScore = 0;
        }

        public void Win()
        {
            if (!isPlayed) return;
            isPlayed = false;

            Level stats = levelGateway.GetLevelStats(curLevel);
            stats.PutNewScore(curScore);
            stats.Complate();
            levelGateway.PutLevelStats(curLevel, stats);
            StopImpl();
            renderService.FinishByWin(WinAfterFinish);
        }

        public void WinAfterFinish()
        {
            foreach (var winCallback in winCallbacks)
            {
                winCallback();
            }
        }

        public void Lose()
        {
            if (!isPlayed) return;
            isPlayed = false;

            Level stats = levelGateway.GetLevelStats(curLevel);
            stats.PutNewScore(curScore);
            levelGateway.PutLevelStats(curLevel, stats);
            StopImpl();
            renderService.FinishByLose(LoseAfterFinish);
        }

        public void LoseAfterFinish()
        {
            foreach (var loseCallback in loseCallbacks)
            {
                loseCallback();
            }
        }

        void IScoreView.Update(int points)
        {
            curScore = points;
        }

        public void AddWinCallback(System.Action winCallback)
        {
            winCallbacks.Add(winCallback);
        }

        public void AddLoseCallback(System.Action loseCallback)
        {
            loseCallbacks.Add(loseCallback);
        }
    }
}
