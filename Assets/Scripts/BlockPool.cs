using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    public GameObject blockPrefab;
    public int initialSize = 200;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(blockPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            GameObject obj = Instantiate(blockPrefab, transform);
            return obj;
        }

        GameObject block = pool.Dequeue();
        block.SetActive(true);
        return block;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
