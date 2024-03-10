using UnityEngine;
using UnityEngine.UI;

public class PlateSingleUI : MonoBehaviour
{
    public Image image;
    public void SetupUI(Sprite igredientSprite)
    {
        image.sprite = igredientSprite;
        image.gameObject.SetActive(true);
    }
    public void SetDefaultUI()
    {
        image.sprite = null;
        image.gameObject.SetActive(false);
    }
}
