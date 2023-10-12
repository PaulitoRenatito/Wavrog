using System;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class WaveCountdownUI : MonoBehaviour
{
    
    private const string NUMBER_POPUP = "NumberPopup";
    
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private Animator animator;

    private int previusCountdownNumber;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        WaveManager.Instance.OnStateChanged += WaveManager_OnStateChanged;

        Hide();
    }

    private void WaveManager_OnStateChanged(object sender, WaveManager.OnStateChangedEventsArgs e)
    {
        if (e.state == WaveManager.State.CountdownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    
    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(WaveManager.Instance.Timer);
        countdownText.text = countdownNumber.ToString();

        if (previusCountdownNumber != countdownNumber)
        {
            previusCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
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
