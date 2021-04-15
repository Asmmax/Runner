using UnityEngine;
using Core.Game;
using UnityEngine.UI;

public class TextHealthView : MonoBehaviour, IHealthView
{
    [SerializeField]
    private Text text;

    void IHealthView.Update(int points)
    {
        text.text = points.ToString();
    }
}
