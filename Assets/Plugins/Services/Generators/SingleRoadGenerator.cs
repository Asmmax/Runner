using Core.Game;
using Core.Level;
using Services.Spawners;
using System.Collections.Generic;

namespace Services.Generators
{
    public struct DensityTemplate
    {
        public float density;
        public ViewableResource template;
    }

    public struct SingleRoadParams
    {
        public int seed;
        public float[] roadDensity;
        public float[] spaceDensity;
        public DensityTemplate[] roadTemplates;
        public DensityTemplate[] spaceTemplates;
    }

    public class SingleRoadGeneratorFactory : ILevelGeneratorFactory
    {
        private IViewGroupMapper viewGroupMapper;
        private IRandomGenerator randomGenerator;
        private LevelParams levelSettings;
        private SingleRoadParams settings;

        public SingleRoadGeneratorFactory(IViewGroupMapper viewGroupMapper, IRandomGenerator randomGenerator)
        {
            this.viewGroupMapper = viewGroupMapper;
            this.randomGenerator = randomGenerator;
        }
        public void SetupSingleRoad(LevelParams levelSettings, SingleRoadParams settings)
        {
            this.levelSettings = levelSettings;
            this.settings = settings;
        }
        public ILevelGenerator GetLevelGenerator()
        {
            return new SingleRoadGenerator(viewGroupMapper, randomGenerator, levelSettings, settings);
        }
    }

    public class SingleRoadGenerator : ProceduralGenerator
    {
        private SingleRoad singleRoad;
        private IViewGroupMapper viewGroupMapper;
        private IRandomGenerator randomGenerator;
        private SingleRoadParams settings;

        public SingleRoadGenerator(IViewGroupMapper viewGroupMapper, IRandomGenerator randomGenerator, LevelParams levelSettings, SingleRoadParams settings)
            : base(viewGroupMapper, levelSettings)
        {
            this.viewGroupMapper = viewGroupMapper;
            this.randomGenerator = randomGenerator;
            singleRoad = new SingleRoad(randomGenerator);
            this.settings = settings;
        }

        protected override Generator SetupGenerator(Field targetfield, ConverterContainer resourceContainer)
        {
            randomGenerator.Reset(settings.seed);
            singleRoad.TargetField = targetfield;
            singleRoad.RoadDensity = settings.roadDensity;
            singleRoad.SpaceDensity = settings.spaceDensity;

            IDictionary<ViewableResource, SpawningResource> spawningResources = new Dictionary<ViewableResource, SpawningResource>();

            foreach (var resource in settings.roadTemplates)
            {
                if (!spawningResources.ContainsKey(resource.template))
                {
                    resource.template.ViewGroupMapper = viewGroupMapper;
                    spawningResources.Add(resource.template, new SpawningResource(resource.template));
                }
            }
            foreach (var resource in settings.spaceTemplates)
            {
                if (!spawningResources.ContainsKey(resource.template))
                {
                    resource.template.ViewGroupMapper = viewGroupMapper;
                    spawningResources.Add(resource.template, new SpawningResource(resource.template));
                }
            }

            foreach (var resource in settings.roadTemplates)
            {
                singleRoad.AddRoadResource(resource.density, spawningResources[resource.template]);
            }

            foreach (var resource in settings.spaceTemplates)
            {
                singleRoad.AddSpaceResource(resource.density, spawningResources[resource.template]);
            }

            resourceContainer.Clear();
            foreach (var resource in spawningResources)
            {
                resourceContainer.AddConverter(resource.Value);
            }

            return singleRoad;
        }
    }
}