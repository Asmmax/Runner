using Core.Game;
using Core.Level;
using Services.Spawners;

namespace Services.Generators
{
    public struct LevelParams
    {
        public uint lineCount;
        public float lineThickness;
        public int lineLength;
        public float startSpeed;
        public float startDistance;
    }

    public abstract class ProceduralGenerator : ILevelGenerator
    {
        private IViewGroupMapper viewGroupMapper;

        private LevelParams levelSettings;

        public ProceduralGenerator(IViewGroupMapper viewGroupMapper, LevelParams levelSettings)
        {
            this.viewGroupMapper = viewGroupMapper;
            this.levelSettings = levelSettings;
        }

        public IEntityConverter Generate()
        {
            Field field = new Field(levelSettings.lineThickness);
            Character character = new Character(field, viewGroupMapper, levelSettings.startSpeed, levelSettings.startDistance);

            ConverterContainer resourceContainer = new ConverterContainer();
            Generator curGeneratorSetups = SetupGenerator(field, resourceContainer);

            curGeneratorSetups.LineCount = levelSettings.lineCount;
            curGeneratorSetups.StepSize = levelSettings.lineThickness;
            for (int i = 0; i < levelSettings.lineLength; i++)
                curGeneratorSetups.Step();

            ConverterContainer converterContainer = new ConverterContainer();
            converterContainer.AddConverter(character);
            converterContainer.AddConverter(resourceContainer);

            return converterContainer;
        }

        protected abstract Generator SetupGenerator(Field targetfield, ConverterContainer container);
    }

}