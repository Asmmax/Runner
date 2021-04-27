using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LocalizableText : UnityEvent<string> { }

public class TextLocalizator : MonoBehaviour
{
    [SerializeField]
    private string textId;
    [SerializeField]
    private LocalizableText setText;

    private ITextLocalizationService textLocalization;

    [Zenject.Inject]
    public void Init(ITextLocalizationService textLocalization)
    {
        this.textLocalization = textLocalization;
    }

    private void Awake()
    {
        setText.Invoke(textLocalization.TranslateText(textId));
    }
}
