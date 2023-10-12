using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    private int poolSize = 20;

    private Queue<GameObject> objectPool = new Queue<GameObject>();

    
    private void Start()
    {

        GameObject enemyHolder = new GameObject("Enemy Holder");
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemyHolder.transform);
            enemy.SetActive(false);
            objectPool.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        if (objectPool.Count > 0)
        {
            GameObject enemy = objectPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }

        return Instantiate(enemyPrefab);
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        objectPool.Enqueue(enemy);
    }
    
}
