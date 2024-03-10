using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private float totalCoinOrderDelivery = 0f;
    [SerializeField] private float totalOrdersCountDelivery = 0f;
    [SerializeField] private float totalCoin = 0f;
    [SerializeField] private float totalCoinTip = 0f;
    [SerializeField] private float totalCoinOdersFaled = 0f;
    [SerializeField] private float totalOrdersFailedCountDelivery = 0f;
    private CoinPanelUI coinPanelUI;

    public void HandleGetAllTypeCoin(ref float ordersDelivery, ref float ordersCountDelivery, ref float tips, ref float odersFailed, ref float odersFailedCountFailed, ref float totalCoins)
    {
        totalCoins = totalCoin;
        tips = totalCoinTip;
        odersFailed = totalCoinOdersFaled;
        ordersDelivery = totalCoinOrderDelivery;
        ordersCountDelivery = totalOrdersCountDelivery;
        odersFailedCountFailed = totalOrdersFailedCountDelivery;

    }

    private void Start()
    {
        coinPanelUI = FindFirstObjectByType<CoinPanelUI>();
        //HandleCoinUI();
    }

    public void HandleAddCoin(float coin, int tip)
    {
        if(tip == 0)
        {
            totalCoinOrderDelivery += coin;
            totalOrdersCountDelivery += 1;
            totalCoin += coin;
            HandleCoinUI(true);
        }
        else
        {
            var coinTip = (Mathf.Round((float)(coin / 3)) * tip);
            Debug.Log("RoundToInt" + Mathf.RoundToInt(1 / 3 * coin));
            Debug.Log("tip" + tip);
            totalCoin += coin + coinTip;
            totalCoinTip += coinTip;
            totalCoinOrderDelivery += coin ;
            totalOrdersCountDelivery += 1;
            HandleTipBonusUI(coinTip);
            HandleCoinUI(true);
        }
    }

    public void HandleMinusCoin(float coin)
    {
        totalCoinOdersFaled += coin;
        totalOrdersFailedCountDelivery += 1;
        totalCoin -= coin;
        HandleCoinUI(false);
    }
    
    private void HandleCoinUI(bool isAddCoin)
    {
        coinPanelUI.HandleCoinValueUI(totalCoin, isAddCoin);
    }
    private void HandleTipBonusUI(float tipCoin)
    {
        coinPanelUI.SpawnVfxCoinTipBonus(tipCoin);
    }

}
