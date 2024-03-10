using UnityEngine;
using UnityEngine.UI;

public class KitchenObjName : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite imagekitchenObj;

    public string Name { get => name; }
    public Sprite ImageKitchenObj { get { return imagekitchenObj; } }

   
}
