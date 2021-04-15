namespace Core.Level
{
    public struct Rect
    {
        public float top;
        public float bottom;
        public float right;
        public float left;
    }
    public class Field
    {
        private Rect boundingBox;
        private float lineThickness;

        public Field(float lineThickness)
        {
            this.lineThickness = lineThickness;
        }

        public Rect BoundingBox
        {
            get
            {
                return boundingBox;
            }
        }
        public void Place(Resource resource)
        {
            float xCenter = resource.Distance;
            float yCenter = lineThickness * resource.Line;
            float absHeight = (yCenter > 0 ? yCenter : -yCenter) + resource.Size.y/2;

            if (boundingBox.top < absHeight)
            {
                boundingBox.top = absHeight;
                boundingBox.bottom = -absHeight;
            }

            if (boundingBox.left == 0 && boundingBox.right == 0)
            {
                boundingBox.left = xCenter - resource.Size.x / 2;
                boundingBox.right = xCenter + resource.Size.y / 2;
            }
            else
            {
                if (boundingBox.left > xCenter - resource.Size.x / 2)
                {
                    boundingBox.left = xCenter - resource.Size.x / 2;
                }
                if (boundingBox.right < xCenter + resource.Size.x / 2)
                {
                    boundingBox.right = xCenter + resource.Size.x / 2;
                }
            }

            resource.Field = this;
            resource.Spawn();

        }

        public float GetHorizontalPosFor(int line)
        {
            return lineThickness * line;
        }
    }
}
