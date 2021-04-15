using Core.Game;
using Core.Data;

namespace Interactors
{
    public interface IConverterGateway
    {
        IEntityConverter GetConverterFor(int level);
    }

    public interface IRenderService
    {
        void Clean();
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
            targetModel.Initialize();
        }

        public void Stop()
        {
            if (!isPlayed) return;
            isPlayed = false;

            targetModel.Dispose();
            inputController.Reset();
            inputController.Lock();
            renderService.Clean();
            curScore = 0;
        }

        public void Win()
        {
            Level stats = levelGateway.GetLevelStats(curLevel);
            stats.PutNewScore(curScore);
            stats.Complate();
            levelGateway.PutLevelStats(curLevel, stats);
            Stop();
        }

        public void Lose()
        {
            Level stats = levelGateway.GetLevelStats(curLevel);
            stats.PutNewScore(curScore);
            levelGateway.PutLevelStats(curLevel, stats);
            Stop();
        }

        void IScoreView.Update(int points)
        {
            curScore = points;
        }
    }
}
