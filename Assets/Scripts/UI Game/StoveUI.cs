using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoveUI : MonoBehaviour
{
    [Header("Bar time Cooking")]
    [SerializeField] private Image BGBarTimeUI;
    [SerializeField] private Image barTimeUI;
    [Header("Icon Danger Icon")]
    [SerializeField] private Image dangerIcon;
    [SerializeField] private float timeAvailableDangerIcon = 10f;
    [Header("Icon Tick Done")]
    [SerializeField] private Image BGcookingDoneIcon;
    [SerializeField] private Image cookingDoneIcon;

    private float timer;
    private bool isActive;
    private bool waitVfxDone = true;
    private void Start()
    {
        StartModifier();
    }
    private void StartModifier()
    {
        timer = 0;
        barTimeUI.fillAmount = 0;
        dangerIcon.enabled = false;
        barTimeUI.enabled = false;
        BGBarTimeUI.enabled = false;
        BGcookingDoneIcon.enabled = false;
        cookingDoneIcon.enabled = false;
    }
    public bool HandlefryingTimeBar(float fryingTimer, float timeCooking, bool active)
    {   
        if(active)
        {
            BGBarTimeUI.enabled = true;
            barTimeUI.enabled = true;
            barTimeUI.fillAmount = fryingTimer / timeCooking;
            return false;
        }
        else
        {
            BGBarTimeUI.enabled = false;
            barTimeUI.enabled = false;
            return true;
        }
    }
    public void HandleDangerIcon(bool active)
    {
        if (active)
        {
            timer = 0;
            dangerIcon.enabled = true;
            isActive = true;
        }
        else
        {
            dangerIcon.enabled = false;
            isActive = false;
        }
    }
    public void HandleCookingDoneICon()
    {
        cookingDoneIcon.enabled = true;
        BGcookingDoneIcon.enabled = true;
        SoundManager.instance.HandlePlaySound("cookingDone", 1);
        cookingDoneIcon.DOFade(255, 1f).OnComplete(() =>
        {
            cookingDoneIcon.DOFade(0, 1f);
            cookingDoneIcon.enabled = false;
        });
        BGcookingDoneIcon.DOFade(255, 1f).OnComplete(() =>
        {
            BGcookingDoneIcon.DOFade(0, 1f);
            BGcookingDoneIcon.enabled = false;
        });
    }
    private void Update()
    {
        if (!isActive || !waitVfxDone) return;
        if (timer < timeAvailableDangerIcon )
        {
            timer += Time.deltaTime;
            waitVfxDone = false;
            SoundManager.instance.HandlePlaySound("danger",1);
            dangerIcon.DOFade(0, 0.3f).OnComplete(() =>
            {
                dangerIcon.DOFade(255f, 0.3f);
                waitVfxDone = true;
            });
        }

    }
}
