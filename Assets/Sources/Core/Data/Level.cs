namespace Core.Data
{
    public class Level
    {
        private int id = 0;
        private string name;
        private int maxScore = 0;
        private bool isComplated = false;

        public Level(int id)
        {
            this.id = id;
        }

        public int MaxScore
        {
            get
            {
                return maxScore;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
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