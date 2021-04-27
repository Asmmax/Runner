using Services.Saves;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevelFactory : MonoBehaviour, IStateViewFactory
{
    [SerializeField]
    private GameObject levelButtonPref;

    [SerializeField]
    private GameObject levelPanel;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private GameController gameController;

    private ITextLocalizationService textLocalization;

    [Zenject.Inject]
    public void Init(ITextLocalizationService textLocalization)
    {
        this.textLocalization = textLocalization;
    }

    public IStateView GetStateView(int level)
    {
        GameObject levelButton = Instantiate(levelButtonPref, transform);
        Button button = levelButton.GetComponent<Button>();
        button.onClick.AddListener(() => gameController.SetTargetlevel(level));
        button.onClick.AddListener(gameController.Play);
        button.onClick.AddListener(() => levelPanel.SetActive(false));
        button.onClick.AddListener(() => gamePanel.SetActive(true));

        TextLocalizator[] localizators = levelButton.GetComponentsInChildren<TextLocalizator>();
        foreach (var localizator in localizators)
        {
            localizator.Init(textLocalization);
        }

        return levelButton.GetComponent<IStateView>();
    }
}
