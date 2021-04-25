using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saves;

[System.Serializable]
public struct LevelViewPair
{
    public int level;
    public string name;
    public GameObject stateViewNode;
}

public class LevelContainerView : MonoBehaviour, IStateViewContainer, ILevelNaming
{
    [SerializeField]
    private LevelViewPair[] stateViews;

    IDictionary<int, IStateView> stateViewMap = new Dictionary<int, IStateView>();
    IDictionary<int, string> levelNameMap = new Dictionary<int, string>();

    void Awake()
    {
        foreach(var stateView in stateViews)
        {
            IStateView view = stateView.stateViewNode.GetComponent<IStateView>();
            if (view != null)
            {
                stateViewMap.Add(stateView.level, view);
                levelNameMap.Add(stateView.level, stateView.name);
            }
        }
    }

    public int[] GetLevelIDs()
    {
        int[] ids = new int[stateViewMap.Count];
        stateViewMap.Keys.CopyTo(ids, 0);
        return ids;
    }

    public string GetName(int level)
    {
        return levelNameMap[level];
    }

    public IStateView GetStateView(int level)
    {
        return stateViewMap[level];
    }
}
