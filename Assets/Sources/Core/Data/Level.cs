namespace Core.Data
{
    public class Level
    {
        private int maxScore = 0;
        private int stage = 0;
        private bool isComplated = false;

        public Level(int stage)
        {
            this.stage = stage;
        }

        public int MaxScore
        {
            get
            {
                return maxScore;
            }
        }

        public int Stage
        {
            get
            {
                return stage;
            }
        }

        public bool IsComplated()
        {
            return isComplated;
        }

        public void Complate()
        {
            isComplated = true;
        }

        public void PutNewScore(int points)
        {
            if (points > maxScore)
                maxScore = points;
        }
    }
}