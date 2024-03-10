using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    public GameObject selectedVisual;

    private void Awake()
    {
        PlayerController.UnSelectedCounter += Selected;
    }
    private void OnDestroy()
    {
        PlayerController.UnSelectedCounter -= Selected;
    }
    public void Selected(bool value)
    {
        selectedVisual.SetActive(value);
    }
}
