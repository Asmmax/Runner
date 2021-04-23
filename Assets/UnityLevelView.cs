using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactors;
using UnityEngine.UI;

public class UnityLevelView : MonoBehaviour, IInfoViewFactory, IInfoView
{
    [SerializeField]
    private Text scoreField;
    [SerializeField]
    private Text complatenessField;

    [SerializeField]
    private string complateMessage;
    [SerializeField]
    private string notcomplateMessage;

    public IInfoView GetInfoView(int level)
    {
        return this;
    }

    public void UpdateCompleteness(bool isComplated)
    {
        if (isComplated) {
            complatenessField.text = complateMessage;
        }
        else
        {
            complatenessField.text = notcomplateMessage;
        }
    }

    public void UpdateMaxScore(int points)
    {
        scoreField.text = points.ToString();
    }
}
