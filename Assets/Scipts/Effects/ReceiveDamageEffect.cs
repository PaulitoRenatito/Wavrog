using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamageEffect : MonoBehaviour
{
    
    [SerializeField] private Transform receiveDamagePrefab;
    private Health health;
    
    private void Awake()
    {
        health = GetComponent<Health>();
    }
    
    private void OnEnable()
    {
        health.OnCurrentHealthChange += Health_OnHealthChange;
    }

    private void OnDisable()
    {
        health.OnCurrentHealthChange -= Health_OnHealthChange;
    }
    
    private void Health_OnHealthChange(object sender, Health.OnHealthChangeEventsArgs e)
    {
        if (e.gameObject != this.gameObject) return;
        
        if (e.currentHealth > 0)
        {
            Vector3 instatiatePosition = new Vector3(transform.position.x, 0f,
                transform.position.z);
            Instantiate(receiveDamagePrefab, instatiatePosition, Quaternion.identity);
        }
    }
}
