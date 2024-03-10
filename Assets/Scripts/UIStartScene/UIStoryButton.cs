using UnityEngine;

public class UIStoryButton : UIStartBase
{
    public Transform TabLevels;
    private void Reset()
    {
        TabLevels = transform.Find("TabLevel");
    }
    public  void OpenTabLevels()
    {
        TabLevels.gameObject.SetActive(true);
    }
    public void CloseTabLevels()
    {
        TabLevels.gameObject.SetActive(false);
    }
}
