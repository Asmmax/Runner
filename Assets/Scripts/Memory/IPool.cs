using UnityEngine;

public interface IPool
{
    GameObject Allocate(GameObject prefab);
    void Deallocate(GameObject gameObject);
}
