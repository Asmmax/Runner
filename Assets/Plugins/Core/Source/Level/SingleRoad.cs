﻿using System.Collections.Generic;

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
        private int curRoadStep = 0;
        private int curSpaceStep = 0;

        public float[] RoadDensity { set; get; }
        public float[] SpaceDensity { set; get; }

        public Field TargetField
        {
            set { targetField = value; }
        }

        public SingleRoad(IRandomGenerator randGenerator, float startDistance = 0) : base(startDistance)
        {
            this.randGenerator = randGenerator;
            RoadDensity = new float[] { 1 };
            SpaceDensity = new float[] { 1 };
        }

        public void AddSpaceResource(float density, Resource resource)
        {
            if (!spaceGroup.ContainsKey(resource))
            {
                spaceGroup.Add(resource, 0);
            }

            spaceGroup[resource] += density;
            spaceProbSum += density;
        }

        public void AddRoadResource(float density, Resource resource)
        {
            if (!roadGroup.ContainsKey(resource))
            {
                roadGroup.Add(resource, 0);
            }

            roadGroup[resource] += density;
            roadProbSum += density;
        }

        protected override void Build()
        {
            Move();

            float curRoadDensity = RoadDensity[curRoadStep++];
            if (curRoadStep >= RoadDensity.Length) 
                curRoadStep = 0;

            float curSpaceDensity = SpaceDensity[curSpaceStep++];
            if (curSpaceStep >= SpaceDensity.Length) 
                curSpaceStep = 0;

            int halfLine = (int)LineCount >> 1;

            for (int line = -halfLine; line <= halfLine; line++)
            {
                Resource resource = null;
                if (line == curLine)
                {
                    if (randGenerator.GenUniformFloat() <= curRoadDensity)
                        resource = SampleResource(roadGroup, roadProbSum);
                }
                else
                {
                    if (randGenerator.GenUniformFloat() <= curSpaceDensity)
                        resource = SampleResource(spaceGroup, spaceProbSum);
                }

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
            int halfLine = (int)LineCount >> 1;

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

            float value = randGenerator.GenUniformFloat() * normalizeFactor;
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
