using System;
using UnityEngine;

[SelectionBase]
public class Health : MonoBehaviour, IDamageable
{
    public event EventHandler<OnHealthChangeEventsArgs> OnTotalHealthChange;
    public event EventHandler<OnHealthChangeEventsArgs> OnCurrentHealthChange;
    public event EventHandler<OnHealthChangeEventsArgs> OnCurrentHealthEmpty;
    public class OnHealthChangeEventsArgs : EventArgs
    {
        public GameObject gameObject;
        public int totalHealth;
        public int currentHealth;
    }

    [SerializeField] private bool isAlive = true;
    [SerializeField] private int totalHealth = 10;
    [SerializeField] private int currentHealth;

    public bool IsAlive => isAlive;

    public int TotalHealth
    {
        get => totalHealth;
        set
        {
            totalHealth = value;
            OnTotalHealthChange?.Invoke(this, new OnHealthChangeEventsArgs()
            {
                gameObject = this.gameObject,
                totalHealth = totalHealth,
                currentHealth = currentHealth
            });
        }
    }


    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            OnCurrentHealthChange?.Invoke(this, new OnHealthChangeEventsArgs()
            {
                gameObject = this.gameObject,
                totalHealth = totalHealth,
                currentHealth = currentHealth
            });
        }
    }

    private void Awake()
    {
        currentHealth = totalHealth;
    }

    public void ReceiveDamage(Damage damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage.RawDamage, 0);
        
        OnCurrentHealthChange?.Invoke(this, new OnHealthChangeEventsArgs()
        {
            gameObject = this.gameObject,
            totalHealth = totalHealth,
            currentHealth = currentHealth
        });

        if (currentHealth == 0)
        {
            OnCurrentHealthEmpty?.Invoke(this, new OnHealthChangeEventsArgs()
            {
                gameObject = this.gameObject,
                totalHealth = totalHealth,
                currentHealth = currentHealth
            });
            isAlive = false;
        }
        
    }

    public void ResetHealth()
    {
        currentHealth = totalHealth;
    }
}
