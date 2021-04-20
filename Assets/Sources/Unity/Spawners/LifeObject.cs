using Services.Spawners;
using UnityEngine;

[CreateAssetMenu(fileName = "Life", menuName = "Resources/Life")]
public class LifeObject : ViewableObject
{
    public uint count;

    private Life life;

    protected override ViewableResource GetViewableResourceImpl()
    {
        if (life == null)
        {
            life = new Life();
        }
        life.Count = count;
        return life;
    }
}
