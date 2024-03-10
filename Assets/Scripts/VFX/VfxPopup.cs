using TMPro;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.UI.Image;

public class VfxPopup : MonoBehaviour
{   
   public void HandleVfxMoveUp(TextMeshProUGUI text,float timeAvailable, Vector3 initPos, Vector3 directMove)
    {
        text.rectTransform.DOScale(Vector3.one * 1.2f, timeAvailable * 5).OnComplete(() => text.rectTransform.DOScale(Vector3.one, 0f));

        text.DOFade(1, timeAvailable * 4).OnComplete(() => text.DOFade(0f, timeAvailable));

        text.rectTransform.DOMove(initPos + directMove * 1, timeAvailable * 5).OnComplete(() => text.rectTransform.DOMove(initPos, timeAvailable));
    }

    public void HandleVfxFailedShake(TextMeshProUGUI text,float timeAvailable, Color originalColor, Vector3 ShakeStrength)
    {
        text.rectTransform.DOShakePosition(timeAvailable * 3, ShakeStrength);

        text.DOColor(Color.red, timeAvailable * 3).OnComplete(() => text.DOColor(originalColor, timeAvailable));
    }

    public void HandleVfxCorrectScale(TextMeshProUGUI text, float sizeScale , float timeAvailable, Color originalColor)
    {
        text.rectTransform.DOScale(Vector3.one * sizeScale, timeAvailable * 3).OnComplete(() => text.rectTransform.DOScale(Vector3.one, timeAvailable));

        text.DOColor(Color.green, timeAvailable * 3).OnComplete(() => text.DOColor(originalColor, timeAvailable));
    }
}
