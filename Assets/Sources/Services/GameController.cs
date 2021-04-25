using Core.Game;
using Core.Data;
using System.Collections.Generic;
using Saves;

namespace Interactors
{
    public interface IConverterGateway
    {
        IEntityConverter GetConverterFor(int level);
    }

    public class GameController :  IGameController, IScoreView
    {
        bool isPlayed = false;

        int curLevel = 0;
        int curScore = 0;
        IConverterGateway converterGateway;
        ILevelGateway levelGateway;
        IInputController inputController;
        ITimeController timeController;

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

        public GameController(IConverterGateway converterGateway, 
            ILevelGateway levelGateway, 
            ITimeController timeController, 
            IInputController inputController)
        {
            this.converterGateway = converterGateway;
            this.levelGateway = levelGateway;
            this.inputController = inputController;
            this.timeController = timeController;

            inputController.Lock();
        }

        public void Play(int level)
        {
            if (isPlayed) return;
            isPlayed = true;

            inputController.Unlock();

            curLevel = level;
            IEntityConverter entityConverter = converterGateway.GetConverterFor(level);
            targetModel.EntityConverter = entityConverter;

            targetModel.Initialize();
        }

        public void Pause()
        {
            timeController.Lock();
            inputController.Lock();
        }

        public void Resume()
        {
            timeController.Unlock();
            inputController.Unlock();
        }

        public void Update()
        {
            if(targetModel != null && targetModel.IsValid())
            {
                targetModel.Update();
            }
        }

        public void Stop()
        {
            if (!isPlayed) return;
            isPlayed = false;

            StopImpl();
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
            levelGateway.PutLevelStats(stats);
            StopImpl();
            foreach (var winCallback in winCallbacks)
                winCallback();
        }

        public void Lose()
        {
            if (!isPlayed) return;
            isPlayed = false;

            Level stats = levelGateway.GetLevelStats(curLevel);
            stats.PutNewScore(curScore);
            levelGateway.PutLevelStats(stats);
            StopImpl();

            foreach (var loseCallback in loseCallbacks)
                loseCallback();
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
