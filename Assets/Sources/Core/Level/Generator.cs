namespace Core.Level
{

    public abstract class Generator
    {
        private int lineCount;
        private float distance;

        protected int LineCount { get { return lineCount; } }
        protected float Distance { get { return distance; } }

        public float StepSize { set; get; }

        public Generator(uint lineCount, float startDistance = 0)
        {
            this.lineCount = (int)(((lineCount >> 1) << 1) + 1);
            distance = startDistance;
        }

        public void Step()
        {
            distance += StepSize;
            Build();
        }

        protected abstract void Build();
    }
}
