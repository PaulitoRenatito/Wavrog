using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    
    public static WaveManager Instance { get; private set; }

    public event EventHandler<OnStateChangedEventsArgs> OnStateChanged;
    public class OnStateChangedEventsArgs : EventArgs
    {
        public State state;
    }
    
    public event EventHandler OnLastEnemyDefeated;
    public event EventHandler<OnEnemySpawnedEventArgs> OnEnemySpawned;
    public class OnEnemySpawnedEventArgs : EventArgs
    {
        public GameObject gameObject;
    }

    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        WaveRunnig
    }
    
    [SerializeField] private int waveCount = 1;
    [SerializeField]private List<GameObject> enemies;
    [SerializeField] private AnimationCurve enemyCountCurve;
    
    [ReadOnly][SerializeField] private State state;
    [SerializeField] private float countdownToStartTimer = 3f;
    [ReadOnly][SerializeField] private float timer;

    private ObjectPool enemyPool;

    public int WaveCount => waveCount;

    public float Timer => timer;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    
    private void Start()
    {
        timer = countdownToStartTimer;
        enemies = new List<GameObject>();
        enemyPool = GetComponent<ObjectPool>();
        GameInput.Instance.OnInteractAlternativeAction += GameInput_OnInteractAlternativeAction;
    }
    
    private void GameInput_OnInteractAlternativeAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs()
            {
                state = state
            });
        }
    }

    private void Update()
    {
        if (state == State.CountdownToStart)
        {
            timer -= Time.deltaTime;

            if (timer < 0f)
            {
                timer = countdownToStartTimer;
                StartWave();
            }
        }
    }

    private void StartWave()
    {
        state = State.WaveRunnig;
        OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs()
        {
            state = state
        });
        SpawnEnemies();
    }

    private void EndWave()
    {
        waveCount++;
        state = State.WaitingToStart;
        OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs()
        {
            state = state
        });
    }

    private void SpawnEnemies()
    {
        int enemyCount = (int) enemyCountCurve.Evaluate(waveCount);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-15.0f, 15.0f), 0f, Random.Range(-15.0f, 15.0f));
            
            Health enemyHealth = enemyPool.GetPoolObject().GetComponent<Health>();
            enemyHealth.transform.position = spawnPosition;
            
            OnEnemySpawned?.Invoke(this, new OnEnemySpawnedEventArgs()
            {
                gameObject = enemyHealth.gameObject
            });

            enemyHealth.OnCurrentHealthEmpty += Health_OnCurrentHealthEmpty;

            enemies.Add(enemyHealth.gameObject);
        }
        
    }

    private void Health_OnCurrentHealthEmpty(object sender, Health.OnHealthChangeEventsArgs e)
    {
        Health enemyHealth = e.gameObject.GetComponent<Health>();
        enemyHealth.ResetHealth();
        enemyHealth.OnCurrentHealthEmpty -= Health_OnCurrentHealthEmpty;
        enemyPool.ReturnPoolObject(e.gameObject);
        enemies.Remove(e.gameObject);
        
        if (enemies.Count == 0)
        {
            OnLastEnemyDefeated?.Invoke(this, EventArgs.Empty);
            EndWave();
        }
    }
}
