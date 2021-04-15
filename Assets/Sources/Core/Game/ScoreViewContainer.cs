using System.Collections.Generic;

namespace Core.Game
{
    public class ScoreViewContainer : IScoreView
    {
        private IList<IScoreView> scoreViews;
        public ScoreViewContainer(IList<IScoreView> scoreViews)
        {
            this.scoreViews = scoreViews;
        }
        public void Update(int points)
        {
            foreach(var scoreView in scoreViews)
            {
                scoreView.Update(points);
            }
        }
    }
}