using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsButton : UIStartBase
{
    public GameObject tabSettings;

    public void ShowTabSettings()
    {
        tabSettings.SetActive(true);
    }

    public void CloseTabSettings()
    {
        tabSettings.SetActive(false);
    }

}
