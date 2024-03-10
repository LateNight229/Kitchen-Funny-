using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryCounterUI : MonoBehaviour
{
    public TextMeshProUGUI correctDishText;

    private Vector3 posInitVfx;
    private VfxPopup vfxPopup;

    private void Start()
    {
        vfxPopup = FindFirstObjectByType<VfxPopup>();
        posInitVfx = correctDishText.rectTransform.position;
    }
    public void HandleShowCorrectText()
    {
        vfxPopup.HandleVfxMoveUp(correctDishText, 0.2f, posInitVfx, new Vector3(0f,1f,0f));
    }
}
