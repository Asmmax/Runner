using Services.Spawners;
using UnityEngine;

[CreateAssetMenu(fileName = "Veget", menuName = "Resources/Veget")]
public class VegetObject : ViewableObject
{
    public int damage;

    private Veget veget;

    protected override ViewableResource GetViewableResourceImpl()
    {
        if(veget == null)
        {
            veget = new Veget();
        }
        veget.Damage = damage;
        return veget;
    }
}
