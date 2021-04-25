using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saves;
using UnityEngine.UI;

public class ButtonLevelView : MonoBehaviour, IStateView
{
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Button button;
    [SerializeField]
    private int level;

    private string buttonName;
    private int score;
    private bool isCompleted;

    public void SetActive(bool isActive)
    {
        button.interactable = isActive;
    }

    public void SetCompleteness(bool isCompleted)
    {
        this.isCompleted = isCompleted;
        UpdateButtonName();

    }

    public void SetLevelName(string name)
    {
        buttonName = name;
        UpdateButtonName();
    }

    public void SetMaxScore(int points)
    {
        score = points;
        UpdateButtonName();
    }

    private void UpdateButtonName()
    {
        string levelInfo = "";
        if (isCompleted) {
            levelInfo = score.ToString();
        }
        buttonText.text = buttonName + ((levelInfo.Length > 0) ? "\n" + levelInfo : "");
    }
}
