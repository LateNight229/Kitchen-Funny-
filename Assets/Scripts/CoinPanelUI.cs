using TMPro;
using UnityEngine;

public class CoinPanelUI : MonoBehaviour
{
    public TextMeshProUGUI coinValue;
    public TextMeshProUGUI tipValue;

    public void HandleCoinValueUI(float coin)
    {
        coinValue.text = coin.ToString();
    }

    public void HandleTipValueUI(float tip)
    {
        tipValue.text = "tip x " + tip.ToString(); 
    }

}
