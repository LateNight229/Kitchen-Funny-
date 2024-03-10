using UnityEngine;
using UnityEngine.UI;

public class TimerBase : MonoBehaviour
{
    [SerializeField] protected Image timerImage;
    [SerializeField] protected float sumTime;

    protected float timer;

    public virtual float Timer {  get { return timer; } set {timer = value; } }
    protected virtual void Start()
    {
        timer = 0f;
        if(sumTime == 0)
        {
            Debug.LogError("SumTime = 0");
        }
    }

    protected virtual void LateUpdate()
    {
        if( timer >= sumTime)
        {
            HandleEndTime();
        }
        if( timer < sumTime )
        {
            timer += Time.deltaTime;
            var percent = HandleCalculatePercent(timer);
            HandleTimerBarUI(timerImage, percent);
        }
    }

    public virtual void HandleEndTime()
    {
    }

    protected virtual float HandleCalculatePercent(float timer)
    {
        return timer / sumTime;
    }

    public virtual void HandleTimerBarUI(Image timerBar, float percentValue)
    {
        timerBar.fillAmount  = 1 - percentValue;
    }
}
