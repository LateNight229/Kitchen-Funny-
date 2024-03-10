using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class VisualUIOrder : MonoBehaviour
{
    public Image correctDish;
    public GameObject FailedDish;

    public void HandleVisualUIOrder(bool isCorrect, float timeShowVisualCorrect)
    {
        if(isCorrect) 
        {
            correctDish.DOColor(endValue: Color.green, timeShowVisualCorrect).OnComplete(() =>
            {
                correctDish.DOColor(endValue: Color.white, 0.1f);
            });
            correctDish.DOFade(0.5f, timeShowVisualCorrect).OnComplete(() => correctDish.DOFade(0f, 0.1f));


        }
        else
        {
            correctDish.DOColor(endValue: Color.red, timeShowVisualCorrect).OnComplete(() =>
            {
                correctDish.DOColor(endValue: Color.white, 0.1f);
            });
            correctDish.DOFade(0.5f, timeShowVisualCorrect).OnComplete(() => correctDish.DOFade(0f, 0.1f));
        }
    }
}
