using Core.Game;
using Interactors;
using System.Collections.Generic;
using Services.Generators;

public class UnityGeneratorContainer : IConverterGateway
{
    private GeneratorObject[] generators;
    private IList<ILevelGeneratorFactory> generatorFactories;

    public UnityGeneratorContainer(GeneratorObject[] generators, IList<ILevelGeneratorFactory> generatorFactories)
    {
        this.generators = generators;
        this.generatorFactories = generatorFactories;
    }

    public IEntityConverter GetConverterFor(int level)
    {
        GeneratorObject generator = generators[level];
        if (!generator) return null;

        ILevelGenerator levelGenerator = generator.GetLevelGenerator(generatorFactories);

        if (levelGenerator != null) return levelGenerator.Generate();
        return null;
    }
}
