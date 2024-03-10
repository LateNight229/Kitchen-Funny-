using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneManager : TimerBase
{

    public TextMeshProUGUI numberPecentText;

    private GameManager gameManager;
    private float numberPecent;

    protected override void Start()
    {
        base.Start();
        gameManager = FindFirstObjectByType<GameManager>(); 
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

    }
    public override void HandleEndTime()
    {
        gameManager.LoadSceneDone = true;
        this.gameObject.SetActive(false);
    }
    public override void HandleTimerBarUI(Image timerBar, float percentValue)
    {   
        timerBar.fillAmount = percentValue;
        numberPecent = Mathf.RoundToInt(percentValue * 100) ;
        numberPecentText.text = numberPecent.ToString() + " %";
    }
}
