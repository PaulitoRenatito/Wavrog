using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCountUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private Animator animator;

    private int waveCount;

    private string waveTemplate = "WAVE ";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        WaveManager.Instance.OnStateChanged += WaveManager_OnStateChanged;

        waveCount = WaveManager.Instance.WaveCount;
        countdownText.text = waveTemplate + waveCount;

        Show();
    }

    private void WaveManager_OnStateChanged(object sender, WaveManager.OnStateChangedEventsArgs e)
    {
        if (e.state == WaveManager.State.WaitingToStart)
        {
            waveCount = WaveManager.Instance.WaveCount;
            countdownText.text = waveTemplate + waveCount;
            animator.SetTrigger(NUMBER_POPUP);
            FunctionTimer.Create(() => { animator.ResetTrigger(NUMBER_POPUP); }, .5f);
        }
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
