using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartManager : MonoBehaviour
{
    public List<Image> uIStartBases = new List<Image>();

    public void HandleTabButtonOnClick(Image imageObj)
    {   
        for(int i = 0; i < uIStartBases.Count; i++)
        {
            if(imageObj == uIStartBases[i])
            {
                uIStartBases[i].gameObject.SetActive(true);
            }
            else
            {
                uIStartBases[i].gameObject.SetActive(false);
            }
        }
    }
}
