using System.Collections.Generic;
using UnityEngine;
using Saves;
using Services.Generators;
using Core.Game;
using Interactors;

[System.Serializable]
public struct LevelInfo
{
    public int level;
    public string name;
    public GameObject levelNode;
    public GeneratorObject levelObject;
}

public class LevelContainer: MonoBehaviour, IStateViewContainer, ILevelNaming, IConverterGateway
{
    [SerializeField]
    private LevelInfo[] levels;

    IDictionary<int, IStateView> stateViews = new Dictionary<int, IStateView>();
    IDictionary<int, string> levelNames = new Dictionary<int, string>();
    IDictionary<int, GeneratorObject> levelGenerators = new Dictionary<int, GeneratorObject>();

    private IList<ILevelGeneratorFactory> generatorFactories;

    [Zenject.Inject]
    public void Init(IList<ILevelGeneratorFactory> generatorFactories)
    {
        this.generatorFactories = generatorFactories;

        foreach (var level in levels)
        {
            IStateView view = level.levelNode.GetComponent<IStateView>();
            if (view != null)
            {
                stateViews.Add(level.level, view);
                levelNames.Add(level.level, level.name);
                levelGenerators.Add(level.level, level.levelObject);
            }
        }
    }

    public int[] GetLevelIDs()
    {
        int[] ids = new int[stateViews.Count];
        stateViews.Keys.CopyTo(ids, 0);
        return ids;
    }

    public string GetName(int level)
    {
        return levelNames[level];
    }

    public IStateView GetStateView(int level)
    {
        return stateViews[level];
    }

    public IEntityConverter GetConverterFor(int level)
    {
        GeneratorObject generator = levelGenerators[level];
        if (!generator) return null;

        ILevelGenerator levelGenerator = generator.GetLevelGenerator(generatorFactories);

        if (levelGenerator != null) return levelGenerator.Generate();
        return null;
    }
}
