namespace Core.Level
{
    public abstract class Resource
    {
        public Resource() { }
        protected Resource(Resource original)
        {
            Distance = original.Distance;
            Field = original.Field;
            Line = original.Line;
            Size = original.Size;
            View = original.View;
        }

        public Field Field { set; get; }
        public int Line { set; get; }
        public float Distance { set; get; }
        public float2 Size { set; get; }
        public string View { set; get; }

        public abstract void Spawn();
    }
}
