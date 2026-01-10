using System;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text timerText;

    public float currentTime = 30f;
    private bool active = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;

        currentTime -= Time.deltaTime;

        UpdateTimerUI();

        if (currentTime <= 0)
        {
            StopTimer();
        }
    }

    public void StopTimer()
    {
        active = false;
        currentTime = 0f;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        TimeSpan t = TimeSpan.FromSeconds(currentTime);
        timerText.text = t.ToString(@"mm\:ss");
    }

}
