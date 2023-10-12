using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private List<GameObject> objectPool = new List<GameObject>();

    private void Start()
    {
        
        if (parentTransform == null)
        {
            parentTransform = new GameObject(prefab.name + " Pool Holder").transform;
        }
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parentTransform);
            obj.gameObject.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetPoolObject()
    {
        
        foreach (GameObject poolObject in objectPool)
        {
            if (!poolObject.gameObject.activeSelf)
            {
                poolObject.gameObject.SetActive(true);
                return poolObject;
            }
        }

        GameObject newObj = Instantiate(prefab, parentTransform);
        newObj.gameObject.SetActive(true);
        objectPool.Add(newObj);

        return newObj;
    }

    public void ReturnPoolObject(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    public void ReturnActivePoolObjects()
    {
        foreach (GameObject poolObject in objectPool)
        {
            if (poolObject.gameObject.activeSelf)
            {
                poolObject.gameObject.SetActive(false);
            }
        }
    }
}
