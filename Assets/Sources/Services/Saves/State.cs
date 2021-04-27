using Core.Data;
using System.Collections.Generic;

namespace Saves
{
    public interface ILevelGateway
    {
        /// <returns>Not null reference to Level</returns>
        Level GetOrCreateLevelStats(int level);
        void PutLevelStats(Level stats);
        void ResetStats();
    }

    public interface IStateView
    {
        void SetMaxScore(int points);
        void SetLevelName(string name);
        void SetCompleteness(bool isCompleted);
        void SetActive(bool isActive);
    }

    public interface ILevelNaming
    {
        string GetName(int level);
    }

    public interface IStateViewContainer
    {
        IStateView GetStateView(int level);
        int[] GetLevelIDs();
    }

    public class State
    {
        private ILevelGateway levelGateway;
        private IStateViewContainer stateViewContainer;
        private ILevelNaming levelNaming;

        public State(ILevelGateway levelGateway, IStateViewContainer stateViewContainer, ILevelNaming levelNaming)
        {
            this.levelGateway = levelGateway;
            this.stateViewContainer = stateViewContainer;
            this.levelNaming = levelNaming;
        }
        public void Update()
        {
            int[] ids = stateViewContainer.GetLevelIDs();
            bool lastComplated = true;

            foreach(var id in ids){
                Level level = levelGateway.GetOrCreateLevelStats(id);
                IStateView stateView = stateViewContainer.GetStateView(id);

                bool isComplated = level.IsComplated();
                stateView.SetCompleteness(isComplated);
                stateView.SetActive(lastComplated);
                lastComplated = isComplated;
                
                stateView.SetMaxScore(level.MaxScore);
                stateView.SetLevelName(levelNaming.GetName(id));
            }
        }
    }
}
