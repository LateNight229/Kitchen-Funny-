using System;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static event Action ClickButtonHolding;
    public static event Action ClickButtonChopping;

    public Button HoldingButton;
    public Button ChoppingButton;

    private void Reset()
    {
        var ChoppingButtonObj = GameObject.Find("ChoopingButton").transform;
        ChoppingButton = ChoppingButtonObj.GetComponentInChildren<Button>();

        var holdlingButtonObj = GameObject.Find("HoldButton ").transform;
        HoldingButton = holdlingButtonObj.GetComponentInChildren<Button>();
    }
    
    void OnClickHoldButton()
    {
        HoldButton();
        Debug.Log("OnClick");

    }
    void OnClickChopButton()
    {
        ChopButton();
        Debug.Log("OnClick");
    }

    public void HoldButton()
    {
        if (ClickButtonHolding != null) ClickButtonHolding();
        Debug.Log("HoldButton");
    }
    public void ChopButton()
    {
        if (ClickButtonChopping != null) ClickButtonChopping();
    }

    protected virtual void Update()
    {

    }

    protected virtual void Start()
    {
        HoldingButton.onClick.AddListener(OnClickHoldButton);
        ChoppingButton.onClick.AddListener(OnClickChopButton);
    }
}
