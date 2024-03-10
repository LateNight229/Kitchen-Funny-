using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleIconLock : MonoBehaviour
{
    public Transform IconLock;

    private bool isReseted;

    private void Reset()
    {
        IconLock = transform.Find("IconLock");
        isReseted = true;
    }
    private void Start()
    {
        if(!isReseted)
        {
            IconLock = transform.Find("IconLock"); 
            Debug.LogWarning("Handle Icon lock does not reset !"); 
        }
    }
    public void HandleActiveIconLock(bool value)
    {
        IconLock.gameObject.SetActive(value);
    }
}
