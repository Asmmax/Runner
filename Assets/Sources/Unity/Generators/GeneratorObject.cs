using UnityEngine;
using Services.Generators;
using System.Collections.Generic;

public abstract class GeneratorObject : ScriptableObject
{
    public ILevelGenerator GetLevelGenerator(IList<ILevelGeneratorFactory> generatorFactories)
    {
        ILevelGeneratorFactory generatorFactory = ChooseGeneratorFactory(generatorFactories);
        if (generatorFactory == null) return null; 

        return generatorFactory.GetLevelGenerator();
    }

    protected abstract ILevelGeneratorFactory ChooseGeneratorFactory(IList<ILevelGeneratorFactory> generatorFactories);
}
