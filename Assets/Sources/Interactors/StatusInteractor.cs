using Core.Data;

namespace Interactors
{
    public interface IInfoView
    {
        void UpdateMaxScore(int points);
        void UpdateCompleteness(bool isComplated);
    }

    public interface IInfoViewFactory
    {
        IInfoView GetInfoView(int level);
    }
    public class StatusInteractor
    {
        ILevelGateway levelGateway;
        IInfoViewFactory infoViewFactory;

        public StatusInteractor(ILevelGateway levelGateway, IInfoViewFactory infoViewFactory)
        {
            this.levelGateway = levelGateway;
            this.infoViewFactory = infoViewFactory;
        }

        public void ShowStatistics(int level)
        {
            Level levelData = levelGateway.GetLevelStats(level);
            IInfoView infoView = infoViewFactory.GetInfoView(level);
            infoView.UpdateCompleteness(levelData.IsComplated());
            infoView.UpdateMaxScore(levelData.MaxScore);
        }
    }
}