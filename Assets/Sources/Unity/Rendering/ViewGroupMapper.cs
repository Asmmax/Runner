using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Services.Spawners;

public class ViewGroupMapper : MonoBehaviour, IViewGroupMapper
{
    [System.Serializable]
    struct ViewGroupStruct
    {
        public string name;
        public GameObject viewPrefab;
    }

    [SerializeField]
    ViewGroupStruct[] viewGroups;

    IDictionary<string, ViewGroupStruct> viewMap = new Dictionary<string, ViewGroupStruct>();

    private void Awake()
    {
        foreach(var viewGroup in viewGroups)
        {
            viewMap.Add(viewGroup.name, viewGroup);
        }
    }

    public GameObject GetPrefab(string name)
    {
        return viewMap[name].viewPrefab;
    }

    public GameObject GetPrefab(int id)
    {
        return viewMap.ElementAt(id).Value.viewPrefab;
    }

    public int GetID(string name)
    {
        return viewMap.Keys.ToList().IndexOf(name);
    }
}
