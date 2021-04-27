using Saves;
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
    private UnityGameController gameController;

    public IStateView GetStateView(int level)
    {
        GameObject levelButton = Instantiate(levelButtonPref, transform);
        Button button = levelButton.GetComponent<Button>();
        button.onClick.AddListener(() => gameController.SetTargetlevel(level));
        button.onClick.AddListener(gameController.Play);
        button.onClick.AddListener(() => levelPanel.SetActive(false));
        button.onClick.AddListener(() => gamePanel.SetActive(true));

        return levelButton.GetComponent<IStateView>();
    }
}
