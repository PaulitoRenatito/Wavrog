using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private Health playerHealth;
    private SliderUI healthBarSlider;
    
    private RectTransform rect;

    [SerializeField] private AnimationCurve barSizeByLevel;
    
    private void Awake()
    {
        playerHealth = player.GetComponent<Health>();
        healthBarSlider = GetComponent<SliderUI>();
        rect = GetComponent<RectTransform>();
    }
    
    private void OnEnable()
    {
        playerHealth.OnTotalHealthChange += Health_OnTotalHealthChange;
        playerHealth.OnCurrentHealthChange += Health_OnCurrentHealthChange;
        playerHealth.OnCurrentHealthEmpty += Health_OnCurrentHealthEmpty;
    }

    private void OnDisable()
    {
        playerHealth.OnTotalHealthChange -= Health_OnTotalHealthChange;
        playerHealth.OnCurrentHealthChange -= Health_OnCurrentHealthChange;
        playerHealth.OnCurrentHealthEmpty -= Health_OnCurrentHealthEmpty;
    }
    
    private void Start()
    {
        float lenght = barSizeByLevel.Evaluate(playerHealth.TotalHealth) / 10f;

        rect.anchorMax = new Vector2(lenght, 1f);

        healthBarSlider.SetMaxFillValue(playerHealth.TotalHealth);
        healthBarSlider.SetCurrentValue(playerHealth.CurrentHealth);
    }
    
    private void Health_OnTotalHealthChange(object sender, Health.OnHealthChangeEventsArgs e)
    {
        float lenght = barSizeByLevel.Evaluate(e.totalHealth) / 10f;
        rect.anchorMax = new Vector2(lenght, 1f);
        healthBarSlider.SetMaxFillValue(e.totalHealth);
    }

    private void Health_OnCurrentHealthChange(object sender, Health.OnHealthChangeEventsArgs e)
    {
        if (e.gameObject == player.gameObject)
        {
            healthBarSlider.SetCurrentValue(e.currentHealth);
        }
    }
    
    private void Health_OnCurrentHealthEmpty(object sender, Health.OnHealthChangeEventsArgs e)
    {
        if (e.gameObject == player.gameObject)
        {
            Destroy(this);
        }
    }
}
