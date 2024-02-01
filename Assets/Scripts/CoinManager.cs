using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private float coinValue = 0f;

    private CoinPanelUI coinPanelUI;

    private void Start()
    {
        coinPanelUI = GameObject.FindObjectOfType<CoinPanelUI>();
        HandleCoinUI();
    }

    public void HandleAddCoin(float coin, int tip)
    {
        if(tip == 0)
        {
            coinValue += coin;
        }
        else
        {
          coinValue += (coin * tip);
        }
        HandleCoinUI();
    }

    public void HandleMinusCoin(float coin)
    {
        coinValue -= coin;
        HandleCoinUI();
    }
    
    private void HandleCoinUI()
    {
        coinPanelUI.HandleCoinValueUI(coinValue);
    }

}
