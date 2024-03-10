using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimeGame : TimerBase
{
    [SerializeField] private TextMeshProUGUI minuteText;
    [SerializeField] private float scaleTextTimer = 1.2f;
    public GameObject Clock;
    public GameObject SumaryPanel;

    private float timeAvailable;
    private Color originalColor = Color.white;
    private bool isCompleteVfx = true;
    private bool isFinishedVfx = false;
    private bool isEndTime;

    protected override void Start()
    {
        base.Start();
        var minuteTranform = transform.Find("value");
        if (minuteTranform != null) { minuteText = minuteTranform.gameObject.GetComponent<TextMeshProUGUI>(); }
        Clock = transform.Find("Clock").gameObject;
    }

    protected override void LateUpdate()
    {
        if (timer >= sumTime && !isEndTime )
        {
            HandleEndTime();
        }
        if (timer < sumTime)
        {
            timer += Time.deltaTime;
            var percent = HandleCalculatePercent(timer);
            HandleTimerBarUI(timerImage, percent);
        }
        timeAvailable = sumTime - timer;
        StrangeSecondToMinute(timeAvailable);
        if (timeAvailable <= 30f)
        {
            VfxTextTimer(); 
            VfxRotateClock();
        }

    }
    public override void HandleEndTime()
    {   
        
        if (Time.timeScale == 0)
        {
            // Resume game
            Time.timeScale = 1;
            Debug.LogError("timeScale" + Time.timeScale);
            SumaryPanel.SetActive(false);
            SoundManager.instance.HandleBGMusic(On: true);
        }
        else
        {
            // Pause game
            isEndTime = true;
            Time.timeScale = 0;
            SumaryPanel.SetActive(true);
            SoundManager.instance.HandleBGMusic(On: false);
        }
    }
    private void StrangeSecondToMinute(float seconds)
    {
        var minute = seconds / 60f;
        var remainingSecond = seconds % 60f;

        minuteText.text = Mathf.FloorToInt(minute).ToString() + " : " + Mathf.RoundToInt(remainingSecond).ToString().ToString();
    }
    void VfxTextTimer()
    {
        if (!isCompleteVfx) return;
        isCompleteVfx = false;

        minuteText.rectTransform.DOScale(Vector3.one * scaleTextTimer, 0.5f).OnComplete(() => minuteText.rectTransform.DOScale(Vector3.one, 0.5f));

        Clock.transform.DOScale(Vector3.one * 1.3f, 0.5f).OnComplete(() => Clock.transform.DOScale(Vector3.one , 0.5f));

        minuteText.DOColor(endValue: Color.red, 0.5f).OnComplete(() =>
        {
        minuteText.DOColor(originalColor, 0.5f).OnComplete(() => isCompleteVfx = true);
        });
    }
    void VfxRotateClock()
    {
        if (isFinishedVfx) return;
        isFinishedVfx = true;
        Clock.transform.DOShakeRotation(duration: 300f, strength: new Vector3(0f, 0f, 30f), vibrato: 10, randomness: 1f, fadeOut: true);
    }

}
