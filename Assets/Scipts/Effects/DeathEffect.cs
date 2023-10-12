using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    
    [SerializeField] private Transform deathEffectPrefab;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnCurrentHealthEmpty += Health_OnHealthEmpty;
    }

    private void OnDisable()
    {
        health.OnCurrentHealthEmpty -= Health_OnHealthEmpty;
    }
    
    private void Health_OnHealthEmpty(object sender, Health.OnHealthChangeEventsArgs e)
    {
        if (e.gameObject == this.gameObject)
        {
            Vector3 instatiatePosition = new Vector3(transform.position.x, 0f,
                transform.position.z);
            Instantiate(deathEffectPrefab, instatiatePosition, Quaternion.identity);
        }
    }
}
