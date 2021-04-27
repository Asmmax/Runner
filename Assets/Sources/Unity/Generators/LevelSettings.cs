using UnityEngine;

[System.Serializable]
public struct LevelInfo
{
    public int id;
    public string name;
    public GeneratorObject levelObject;
}

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/LevelSettings")]
public class LevelSettings: ScriptableObject
{
    public LevelInfo[] levels;
}
