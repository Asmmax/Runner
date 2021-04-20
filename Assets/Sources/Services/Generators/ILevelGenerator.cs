using Core.Game;

namespace Services.Generators
{
    public interface ILevelGenerator
    {
        IEntityConverter Generate();
    }

    public interface ILevelGeneratorFactory
    {
        ILevelGenerator GetLevelGenerator();
    }
}