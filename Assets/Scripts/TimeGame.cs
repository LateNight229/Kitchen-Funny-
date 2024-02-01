using TMPro;
using UnityEngine;

public class TimeGame : TimerBase
{
    [SerializeField] private TextMeshProUGUI minuteText;
    private float timeAvailable;

    protected override void HandleEndTime()
    {
        Debug.Log("End Time !");
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        timeAvailable = sumTime - timer;
        StrangeSecondToMinute(timeAvailable);
    }

    private void StrangeSecondToMinute(float seconds)
    {
        var minute = seconds / 60f;
        var remainingSecond = seconds % 60f;

        minuteText.text = Mathf.FloorToInt(minute).ToString() + " : " + Mathf.RoundToInt(remainingSecond).ToString().ToString();
    }

}
