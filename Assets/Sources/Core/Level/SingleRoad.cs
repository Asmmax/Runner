using System.Collections.Generic;

namespace Core.Level
{
    public interface IRandomGenerator
    {
        int GenInt(int maxValue);
        float GenUniformFloat();

        void Reset(int seed);

    }

    public class SingleRoad : Generator
    {
        private Field targetField;
        private IDictionary<Resource, float> spaceGroup = new Dictionary<Resource, float>();
        private IDictionary<Resource, float> roadGroup = new Dictionary<Resource, float>();
        private float spaceProbSum = 0;
        private float roadProbSum = 0;
        private IRandomGenerator randGenerator;

        private int curLine = 0;

        public float RoadDensity { set; get; }
        public float SpaceDensity { set; get; }

        public SingleRoad(Field targetField, IRandomGenerator randGenerator, uint lineCount, float startDistance = 0) : base(lineCount, startDistance)
        {
            this.targetField = targetField;
            this.randGenerator = randGenerator;
        }

        public void AddSpaceResource(float probability, Resource resource)
        {
            if (!spaceGroup.ContainsKey(resource))
            {
                spaceGroup.Add(resource, 0);
            }

            spaceGroup[resource] += probability;
            spaceProbSum += probability;
        }

        public void AddRoadResource(float probability, Resource resource)
        {
            if (!roadGroup.ContainsKey(resource))
            {
                roadGroup.Add(resource, 0);
            }

            roadGroup[resource] += probability;
            roadProbSum += probability;
        }

        protected override void Build()
        {
            Move();

            int halfLine = LineCount >> 1;

            for (int line = -halfLine; line <= halfLine; line++)
            {
                Resource resource = null;
                if(line == curLine)
                    resource = SampleResource(roadGroup, roadProbSum > 1 ? 1 : roadProbSum);
                else
                    resource = SampleResource(spaceGroup, spaceProbSum > 1 ? 1 : spaceProbSum);

                if (resource != null)
                {
                    resource.Line = line;
                    resource.Distance = Distance;
                    targetField.Place(resource);
                }
            }
        }

        private void Move()
        {
            int step = 0;
            int halfLine = LineCount >> 1;

            if(curLine < halfLine && curLine > -halfLine)
            {
                step = randGenerator.GenInt(2) - 1;
            }
            else if (curLine >= halfLine)
            {
                step = randGenerator.GenInt(1) - 1;
            }
            else if (curLine <= -halfLine)
            {
                step = randGenerator.GenInt(1);
            }

            curLine += step;
        }

        private Resource SampleResource(IDictionary<Resource, float> group, float normalizeFactor)
        {

            float value = randGenerator.GenUniformFloat() / normalizeFactor;
            float probSum = 0;
            foreach(var item in group)
            {
                probSum += item.Value;

                if(value < probSum)
                {
                    return item.Key;
                }
            }

            return null;
        }
    }
}
