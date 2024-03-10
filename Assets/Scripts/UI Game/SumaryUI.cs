using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SumaryUI : MonoBehaviour
{
    public TextMeshProUGUI ordersDeliveryText;
    public TextMeshProUGUI ordersCountDeliveryText;

    public TextMeshProUGUI tipsText;

    public TextMeshProUGUI odersFailedText;
    public TextMeshProUGUI odersFailedCountText;

    public TextMeshProUGUI totalCoinText;

    private void Reset()
    {
        ordersDeliveryText = HandleFindChildGameObj("Orders  Delivery x  ", 1).GetComponent<TextMeshProUGUI>();
        ordersCountDeliveryText = HandleFindChildGameObj("Orders  Delivery x  ", 0).GetComponent<TextMeshProUGUI>();

        odersFailedText = HandleFindChildGameObj("Orders Failed x ",1).GetComponent<TextMeshProUGUI>();
        odersFailedCountText = HandleFindChildGameObj("Orders Failed x ", 0).GetComponent<TextMeshProUGUI>();

        tipsText = HandleFindChildGameObj("Tips ", 0).GetComponent<TextMeshProUGUI>();

        totalCoinText = HandleFindChildGameObj("TotalPanel", 1).GetComponent<TextMeshProUGUI>();
    }
    private Transform HandleFindChildGameObj(string nameObj,int childCount)
    {
        return GameObject.Find(nameObj).transform.GetChild(childCount);
    }
    public void HandleShowUISumary(float ordersDelivery, float ordersCountDelivery, float tips, float odersFailed, float ordersFailedCountDelivery, float totalCoin)
    {
        ordersDeliveryText.text = ordersDelivery.ToString();
        tipsText.text = tips.ToString();
        odersFailedText.text = odersFailed.ToString();
        totalCoinText.text = totalCoin.ToString();
        ordersCountDeliveryText.text =  ordersCountDelivery.ToString();
        odersFailedCountText.text = "- " + ordersFailedCountDelivery.ToString();
    }
}
