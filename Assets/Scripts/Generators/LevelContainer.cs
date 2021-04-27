using System.Collections.Generic;
using Services.Saves;
using Services.Generators;
using Core.Game;
using Zenject;

public interface IStateViewFactory
{
    IStateView GetStateView(int level);
}

public class LevelContainer: IInitializable, IStateViewContainer, ILevelNaming, IConverterGateway
{
    private LevelSettings settings;

    IDictionary<int, IStateView> stateViews = new Dictionary<int, IStateView>();
    IDictionary<int, string> levelNames = new Dictionary<int, string>();
    IDictionary<int, GeneratorObject> levelGenerators = new Dictionary<int, GeneratorObject>();

    private IList<ILevelGeneratorFactory> generatorFactories;
    private ITextLocalizationService textLocalizationService;
    private IStateViewFactory stateViewFactory;

    public LevelContainer(IList<ILevelGeneratorFactory> generatorFactories, 
        ITextLocalizationService textLocalizationService, 
        IStateViewFactory stateViewFactory, 
        LevelSettings settings)
    {
        this.generatorFactories = generatorFactories;
        this.textLocalizationService = textLocalizationService;
        this.stateViewFactory = stateViewFactory;
        this.settings = settings;
    }

    public void Initialize()
    {
        foreach (var level in settings.levels)
        {
            IStateView view = stateViewFactory.GetStateView(level.id);
            if (view != null)
            {
                stateViews.Add(level.id, view);
                levelNames.Add(level.id, level.name);
                levelGenerators.Add(level.id, level.levelObject);
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
        return textLocalizationService.TranslateText(levelNames[level]);
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
