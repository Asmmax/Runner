using UnityEngine;
using Services.Localization;

[CreateAssetMenu(fileName = "LocalizationSettings", menuName = "Settings/LocalizationSettings")]
public class LocalizationSettings : ScriptableObject
{
    public Local defaultLocal = Local.ENG;
    public TextLocalTable[] tables;
}
