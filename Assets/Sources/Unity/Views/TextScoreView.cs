using UnityEngine;
using Core.Game;
using UnityEngine.UI;

public class TextScoreView : MonoBehaviour, IScoreView
{
    [SerializeField]
    private Text text;

    void IScoreView.Update(int points)
    {
        text.text = points.ToString();
    }
}
