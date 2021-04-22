using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolSettings
{
    public GameObject prefab;
    public int volume;
}

public class ConstPool : MonoBehaviour, IPool
{
    [SerializeField]
    private PoolSettings[] poolSettings;

    private IDictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();
    private IDictionary<GameObject, GameObject> clones = new Dictionary<GameObject, GameObject>();
    private void Awake()
    {
        foreach(var poolSets in poolSettings)
        {
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < poolSets.volume; i++)
            {
                GameObject clone = Instantiate(poolSets.prefab, transform);
                clone.name = poolSets.prefab.name + i.ToString();
                clone.SetActive(false);
                pool.Enqueue(clone);
                clones.Add(clone, poolSets.prefab);
            }

            pools.Add(poolSets.prefab, pool);
        }
    }

    public GameObject Allocate(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab)) return null;

        GameObject go = pools[prefab].Dequeue();
        go.SetActive(true);

        return go;
    }

    public void Deallocate(GameObject gameObject)
    {
        if (!clones.ContainsKey(gameObject)) return;

        GameObject prefab = clones[gameObject];
        if (!pools.ContainsKey(prefab)) return;

        gameObject.SetActive(false);
        pools[prefab].Enqueue(gameObject);
    }
}
