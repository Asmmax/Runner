using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Services.Localization;

[System.Serializable]
public class LocalizableText : UnityEvent<string> { }

public class TextLocalizator : MonoBehaviour
{
    [SerializeField]
    private string textId;
    [SerializeField]
    private LocalizableText setText;

    private void Awake()
    {
        TextLocalizationService textLocalization = FindObjectOfType<TextLocalizationService>();
        setText.Invoke(textLocalization.TranslateText(textId));
    }
}
