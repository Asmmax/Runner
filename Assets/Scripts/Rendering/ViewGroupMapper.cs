using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Services.Spawners;
using Zenject;

public class ViewGroupMapper : IInitializable, IViewGroupMapper
{
    ViewSettings settings;

    IDictionary<string, ViewGroupStruct> viewMap = new Dictionary<string, ViewGroupStruct>();

    public ViewGroupMapper(ViewSettings settings)
    {
        this.settings = settings;
    }

    public void Initialize()
    {
        foreach(var viewGroup in settings.viewGroups)
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
