namespace Core.Level
{
    public abstract class Resource
    {
        public Resource() { }
        protected Resource(Resource original)
        {
            Distance = original.Distance;
            Line = original.Line;
            Size = original.Size;
        }

        public int Line { set; get; }
        public float Distance { set; get; }
        public float2 Size { set; get; }

        public abstract void Spawn(Field field);
    }
}
