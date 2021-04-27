using UnityEngine;


[System.Serializable]
public struct ViewGroupStruct
{
    public string name;
    public GameObject viewPrefab;
}

[CreateAssetMenu(fileName = "ViewSettings", menuName = "Settings/ViewSettings")]
public class ViewSettings : ScriptableObject
{
    public ViewGroupStruct[] viewGroups;
}
