using System;
using UnityEngine;

public class CanvasEnemy : MonoBehaviour
{
    private void Start()
    {
        WaveManager.Instance.OnEnemySpawned += WaveManager_OnEnemySpawned;
    }

    private void WaveManager_OnEnemySpawned(object sender, WaveManager.OnEnemySpawnedEventArgs e)
    {
        if (e.gameObject.TryGetComponent(out HealthBar healthbar))
        {
            healthbar.SetCanvas(transform);
        }
    }
}
