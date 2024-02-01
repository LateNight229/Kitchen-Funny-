using UnityEngine;

public class TipsManager : MonoBehaviour
{
    private int tipsValue;   
    
    public int TipsValue {  get { return tipsValue; } set {  tipsValue = Mathf.Clamp(value,0,4); } }
    private CoinPanelUI coinPanelUI;

    private void Start()
    {
        coinPanelUI = GameObject.FindObjectOfType<CoinPanelUI>();
        TipsValue = 0;
        HandleCoinUI();
    }
    public void HandleResetTips()
    {
        TipsValue = 0;
        HandleCoinUI();
    }
    public void HandleAddTip()
    {
        TipsValue += 1;
        HandleCoinUI();
    }
    private void HandleCoinUI()
    {
        coinPanelUI.HandleTipValueUI(tipsValue);
    }
}
