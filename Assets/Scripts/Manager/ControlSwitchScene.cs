using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static Cinemachine.DocumentationSortingAttribute;

public class ControlSwitchScene : MonoBehaviour
{
    public void HandleChangeScene(int level)
    {
        Debug.Log("Control");
            SceneManager.LoadScene(level);
            SoundManager.instance.Reset();
    }
}
