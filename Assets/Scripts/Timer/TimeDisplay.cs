using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    public TimeLoopManager timeManager;
    public TMP_Text timerText;

    void Update()
    {
        timerText.text = Mathf.Ceil(timeManager.currentTime).ToString("00");
    }
}
