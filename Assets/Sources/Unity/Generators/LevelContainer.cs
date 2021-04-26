using System.Collections.Generic;
using UnityEngine;
using Saves;
using Services.Generators;
using Core.Game;
using Interactors;
using Services.Localization;
using UnityEngine.UI;

[System.Serializable]
public struct LevelInfo
{
    public int id;
    public string name;
    public GeneratorObject levelObject;
}

public class LevelContainer: MonoBehaviour, IStateViewContainer, ILevelNaming, IConverterGateway
{
    [SerializeField]
    private GameObject levelButtonPref;

    [SerializeField]
    private GameObject levelContainerNode;

    [SerializeField]
    private GameObject levelPanel;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private UnityGameController gameController;

    [SerializeField]
    private LevelInfo[] levels;

    IDictionary<int, IStateView> stateViews = new Dictionary<int, IStateView>();
    IDictionary<int, string> levelNames = new Dictionary<int, string>();
    IDictionary<int, GeneratorObject> levelGenerators = new Dictionary<int, GeneratorObject>();

    private IList<ILevelGeneratorFactory> generatorFactories;
    private ITextLocalizationService textLocalizationService;

    [Zenject.Inject]
    public void Init(IList<ILevelGeneratorFactory> generatorFactories, ITextLocalizationService textLocalizationService)
    {
        this.generatorFactories = generatorFactories;
        this.textLocalizationService = textLocalizationService;
    }

    private void Awake()
    {
        foreach (var level in levels)
        {
            GameObject levelButton = Instantiate(levelButtonPref, levelContainerNode.transform);
            Button button = levelButton.GetComponent<Button>();
            button.onClick.AddListener(() => gameController.SetTargetlevel(level.id));
            button.onClick.AddListener(gameController.Play);
            button.onClick.AddListener(() => levelPanel.SetActive(false));
            button.onClick.AddListener(() => gamePanel.SetActive(true));

            IStateView view = levelButton.GetComponent<IStateView>();
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
