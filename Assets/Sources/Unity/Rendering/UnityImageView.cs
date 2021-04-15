using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using Interactors;
using Core;

public class UnityImageView : MonoBehaviour, IImageView, IRenderService
{
    ViewGroupMapper groupMapper;

    IDictionary<int, GameObject> views = new Dictionary<int, GameObject>();
    IDictionary<int, Transform> viewTransforms = new Dictionary<int, Transform>();
    IDictionary<int, float2> viewOldPositions = new Dictionary<int, float2>();

    private void Awake()
    {
        groupMapper = GetComponent<ViewGroupMapper>();
    }

    public void Hide(int[] ids)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (!views.ContainsKey(ids[i])) continue;

            viewOldPositions.Remove(ids[i]);
            viewTransforms.Remove(ids[i]);
            GameObject removed = views[ids[i]];
            views.Remove(ids[i]);
            Destroy(removed);
        }
    }

    public void Show(int[] ids, int[] groups)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (!views.ContainsKey(ids[i]))
            {
                GameObject newView = Instantiate(groupMapper.GetPrefab(groups[i]), transform);
                views.Add(ids[i], newView);
                viewTransforms.Add(ids[i], newView.transform);
                viewTransforms[ids[i]].position = Vector2.zero;
                viewOldPositions.Add(ids[i], new float2 { x = 0, y = 0 });
            }
        }
    }

    public void UpdatePosition(int[] ids, float2[] positions)
    {

        float2 tempPos;

        for (int i = 0; i < ids.Length; i++)
        {
            tempPos = viewOldPositions[ids[i]];
            if (positions[i].x != tempPos.x || positions[i].y != tempPos.y)
            {
                viewTransforms[ids[i]].position = new Vector2(positions[i].x, positions[i].y);
                viewOldPositions[ids[i]] = positions[i];
            }
        }
    }

    public void UpdateAnimSpeed(int[] ids, float[] speeds)
    {
        throw new System.NotImplementedException();
    }

    public void HideAll()
    {
        viewOldPositions.Clear();
        viewTransforms.Clear();
        views.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Clean()
    {
        HideAll();
    }
}
