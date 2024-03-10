using UnityEngine;

public class SumaryManager : MonoBehaviour
{
    public float ordersDelivery;
    public float ordersCountDelivery;
    public float tips;
    public float odersFailed;
    public float ordersFailedCountDelivery;
    public float totalCoin;

    private CoinManager coinManager;
    private SumaryUI sumaryUI;
    private void Start()
    {
        coinManager = FindFirstObjectByType<CoinManager>();
        sumaryUI = FindFirstObjectByType<SumaryUI>();
        if (coinManager != null)
        {
            coinManager.HandleGetAllTypeCoin(ref ordersDelivery,ref ordersCountDelivery, ref tips, ref odersFailed,ref ordersFailedCountDelivery, ref totalCoin);
            HanleSumaryUi();
        }
    }

    public void HandleCalculateStar()
    {
    }

    private void HanleSumaryUi()
    {
        if(sumaryUI != null)
        {
            sumaryUI.HandleShowUISumary(ordersDelivery, ordersCountDelivery, tips, odersFailed, ordersFailedCountDelivery, totalCoin);
        }
    }

}
