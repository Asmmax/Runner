using Services.Spawners;
using UnityEngine;

[CreateAssetMenu(fileName = "Sweet", menuName = "Resources/Sweet")]
public class SweetObject : ViewableObject
{
    public int points;

    private Sweet sweet;

    protected override ViewableResource GetViewableResourceImpl()
    {
        if (sweet == null)
        {
            sweet = new Sweet();
        }
        sweet.Points = points;
        return sweet;
    }
}
