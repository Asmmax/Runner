namespace Core.Level
{

    public abstract class Generator
    {
        private int lineCount = 1;
        private float distance;

        public uint LineCount {
            set { lineCount = (int)(((value >> 1) << 1) + 1); }
            protected get { return (uint)lineCount; } 
        }
        protected float Distance { get { return distance; } }

        public float StepSize { set; get; }

        public Generator(float startDistance = 0)
        {
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
