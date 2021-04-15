using Core.Game;
using Interactors;
using Core.Level;

namespace Services.Spawners
{
    public interface IViewGroupMapper
    {
        int GetID(string viewName);
    }

    [System.Serializable]
    public struct LevelSettings
    {
        public float startSpeed;
        public int lineLength;
        public int seed;
    }

    public class TestProceduralSpawner : IConverterGateway
    {
        private IViewGroupMapper groupMapper;
        private IRandomGenerator randomGenerator;
        private uint lineCount;
        private float lineThickness;
        private float size;

        LevelSettings[] levelSettings;

        public TestProceduralSpawner(IViewGroupMapper groupMapper, IRandomGenerator randomGenerator, LineSettings lineSettings, LevelSettings[] levelSettings)
        {
            this.groupMapper = groupMapper;
            this.randomGenerator = randomGenerator;
            this.levelSettings = levelSettings;

            lineCount = lineSettings.lineCount;
            lineThickness = lineSettings.lineThickness;
            size = lineSettings.defaultResourceSize;
        }

        public IEntityConverter GetConverterFor(int level)
        {
            LevelSettings curSettings = levelSettings[level];
            
            ConverterContainer converterContainer = new ConverterContainer();
            Field field = new Field(lineThickness);

            Character character = new Character(field, groupMapper, curSettings.startSpeed, -10);

            ProceduralResource good = new Sweet(groupMapper);
            good.Size = new Core.float2 { x = size, y = size };
            good.View = "good";

            ProceduralResource bad = new Veget(groupMapper);
            bad.Size = new Core.float2 { x = size, y = size };
            bad.View = "bad";

            ProceduralResource life = new Life(groupMapper);
            life.Size = new Core.float2 { x = size, y = size };
            life.View = "life";

            randomGenerator.Reset(curSettings.seed);
            SingleRoad singleRoad = new SingleRoad(field, randomGenerator, lineCount);
            singleRoad.AddRoadResource(0.05f, life);
            singleRoad.AddRoadResource(0.5f, good);
            singleRoad.AddSpaceResource(0.2f, bad);

            singleRoad.StepSize = lineThickness;
            for(int i = 0; i < curSettings.lineLength; i++)
                singleRoad.Step();

            converterContainer.AddConverter(character);
            converterContainer.AddConverter(good);
            converterContainer.AddConverter(bad);
            converterContainer.AddConverter(life);

            return converterContainer;
        }
    }
}
