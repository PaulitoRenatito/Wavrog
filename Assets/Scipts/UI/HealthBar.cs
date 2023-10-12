using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthBarPrefab;
    [SerializeField] private Transform canvas;

    private Transform healthBar;

    private SliderUI healthBarSlider;

    private Camera mainCamera;
    
    private Health health;

    private void Awake()
    {
        mainCamera = Camera.main;
        health = GetComponent<Health>();
        
        health.OnCurrentHealthChange += Health_OnCurrentHealthChange;
        health.OnCurrentHealthEmpty += Health_OnCurrentHealthEmpty;
    }

    private void OnDestroy()
    {
        health.OnCurrentHealthChange -= Health_OnCurrentHealthChange;
        health.OnCurrentHealthEmpty -= Health_OnCurrentHealthEmpty;
    }

    private void Health_OnCurrentHealthChange(object sender, Health.OnHealthChangeEventsArgs e)
    {
        if (e.gameObject == this.gameObject)
        {
            healthBarSlider.SetCurrentValue(e.currentHealth);
        }
    }
    
    private void Health_OnCurrentHealthEmpty(object sender, Health.OnHealthChangeEventsArgs e)
    {
        if (e.gameObject == this.gameObject)
        {
            Destroy(healthBar.gameObject);
        }
    }

    private void Start()
    {
        // healthBar = Instantiate(healthBarPrefab,canvas, true);
        //     
        // healthBarSlider = healthBar.GetComponent<SliderUI>();
        //
        // healthBarSlider.SetMaxFillValue(health.TotalHealth);
        // healthBarSlider.SetCurrentValue(health.CurrentHealth);
        //
        // SetHealthBarSize();
        //
        // SetHealthBarPosition();
    }

    public void SetCanvas(Transform canvas)
    {
        this.canvas = canvas;
        
        healthBar = Instantiate(healthBarPrefab,canvas, true);
            
        healthBarSlider = healthBar.GetComponent<SliderUI>();

        healthBarSlider.SetMaxFillValue(health.TotalHealth);
        healthBarSlider.SetCurrentValue(health.CurrentHealth);
        
        SetHealthBarSize();
        
        SetHealthBarPosition();
    }

    private void FixedUpdate()
    {
        if (healthBar != null)
        {
            SetHealthBarPosition();
            healthBar.LookAt(mainCamera.transform);
        }
    }

    private void SetHealthBarSize()
    {
        RectTransform healthBarRect = healthBar.GetComponent<RectTransform>();
        healthBarRect.sizeDelta = new Vector2(1f, .2f); //TODO: Discover the size pattern
    }
    
    private void SetHealthBarPosition()
    {
        Vector3 heightOffset = Vector3.up * 2;
        healthBar.transform.position  = transform.position + heightOffset;
    }
    
    
}
