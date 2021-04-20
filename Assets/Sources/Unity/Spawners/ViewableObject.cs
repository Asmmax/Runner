using UnityEngine;
using Services.Spawners;


public abstract class ViewableObject : ScriptableObject
{
    public string viewGroup;
    public Vector2 size;

    public ViewableResource GetViewableResource()
    {
        ViewableResource viewableResource = GetViewableResourceImpl();
        viewableResource.SetViewGroup(viewGroup);
        viewableResource.Size = new Core.float2 { x = size.x, y = size.y };
        return viewableResource;
    }

    protected abstract ViewableResource GetViewableResourceImpl();
}
