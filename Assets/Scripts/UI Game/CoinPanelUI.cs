using TMPro;
using UnityEngine;
using DG.Tweening;

public class CoinPanelUI : MonoBehaviour
{
    public TextMeshProUGUI coinValue;
    public TextMeshProUGUI tipValue;
    public GameObject tipBonusObj;
    public GameObject coinObj;
    public TextMeshProUGUI tipBonusValue;

    [SerializeField] private float timeVfxAvailable = 0.15f;
    [SerializeField] private float sizeVfxScale = 2f;
    [SerializeField] private Vector3 VfxShakeStrength = new Vector3(5f,7f,0f);

    private VfxPopup vfxPopup;

    private Color originalColor = Color.white;
    private Vector3 initPos;
    private void Start()
    {
        LoadComponent();
         initPos = tipBonusValue.transform.position;
        tipBonusObj.SetActive(false);
    }
    private void LoadComponent()
    {
        vfxPopup = FindFirstObjectByType<VfxPopup>();
    }
    public void HandleCoinValueUI(float coin, bool isAddCoin)
    {
        coinValue.text = coin.ToString();
        if(isAddCoin)
        {
            VfxAddCoin();
        }
        else
        {
            VfxMinusCoin();
        }
    }

    public void HandleTipValueUI(float tip)
    {   
        tipValue.text = "tip x " + tip.ToString();
    }
    public void SpawnVfxCoinTipBonus(float coinTip)
    {
        tipBonusValue.text = "+ " + coinTip.ToString();

        if(coinTip > 0)
        {
            tipBonusObj.SetActive(true);
            VfxPopupTipBonus();
            
        }
    }
    private void VfxAddCoin()
    {
        vfxPopup.HandleVfxCorrectScale(text: coinValue, sizeVfxScale, timeVfxAvailable, originalColor);
        coinObj.transform.DOShakeRotation(duration: timeVfxAvailable*5f, strength: new Vector3(0f, 30f, 0f), vibrato: 30, randomness: 1f, fadeOut: true);
    }
    private void VfxMinusCoin()
    {
        vfxPopup.HandleVfxFailedShake(text: coinValue, timeVfxAvailable, originalColor, VfxShakeStrength);
    }
    private void VfxPopupTipBonus()
    {
        vfxPopup.HandleVfxMoveUp(text: tipBonusValue, timeVfxAvailable, initPos, new Vector3(0f,0f,1f));
    }


}
