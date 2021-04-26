using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services.Localization;

[System.Serializable]
public struct TextTranslation
{
    public Local local;
    public string text;
}

[System.Serializable]
public struct TextRow
{
    public string id;
    public TextTranslation[] translations;
}

[CreateAssetMenu(fileName = "TextLocalTable", menuName = "Localization/TextLocalTable")]
public class TextLocalTable : ScriptableObject
{
    [SerializeField]
    private TextRow[] textRows = new TextRow[0];

    [System.NonSerialized]
    private bool isInited = false;
    [System.NonSerialized]
    private IDictionary<string, IDictionary<Local, string>> table = new Dictionary<string, IDictionary<Local, string>>();

    private void Init()
    {
        foreach(var textRow in textRows)
        {
            IDictionary<Local, string> translations = new Dictionary<Local, string>();
            foreach(var translation in textRow.translations)
            {
                translations.Add(translation.local, translation.text);
            }
            table.Add(textRow.id, translations);
        }

        isInited = true;
    }

    public string TranslateText(string id, Local local)
    {
        if (!isInited) 
            Init();

        if (!table.ContainsKey(id))
            return null;

        if (!table[id].ContainsKey(local))
            return null;


        return table[id][local];
    }
}
