using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services.Localization;

public class TextLocalizationService : MonoBehaviour, ITextLocalizationService
{
    [SerializeField]
    private Local defaultLocal = Local.ENG;
    [SerializeField]
    private TextLocalTable[] tables;

    private Local targetLocal;

    void Awake()
    {
        targetLocal = defaultLocal;
    }

    public void SetLocal(Local local)
    {
        targetLocal = local;
    }

    public string TranslateText(string id)
    {
        foreach(var table in tables)
        {
            string translated = table.TranslateText(id, targetLocal);
            if (translated != null)
                return translated;
        }
        return null;
    }
}
