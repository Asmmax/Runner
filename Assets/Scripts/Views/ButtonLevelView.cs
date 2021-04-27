using UnityEngine;
using Services.Saves;
using UnityEngine.UI;

public class ButtonLevelView : MonoBehaviour, IStateView
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject scoreNode;
    [SerializeField]
    private Button button;

    public void SetActive(bool isActive)
    {
        button.interactable = isActive;
    }

    public void SetCompleteness(bool isCompleted)
    {
        scoreNode.SetActive(isCompleted);
    }

    public void SetLevelName(string name)
    {
        nameText.text = name;
    }

    public void SetMaxScore(int points)
    {
        scoreText.text = points.ToString();
    }
}
