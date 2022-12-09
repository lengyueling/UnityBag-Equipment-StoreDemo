using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : Singleton<InputMgr>
{
    private bool isStart = false;

    public InputMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }
    private void Update()
    {
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
    }

    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    private void CheckKeyCode(KeyCode key)
    {
        if (!isStart)
        {
            return;
        }
        if (Input.GetKeyDown(key))
        {
            EventCenter.Instance.EventTrigger("GetKeyDown", key);
        }
        if (Input.GetKeyUp(key))
        {
            EventCenter.Instance.EventTrigger("GetKeyUp", key);
        }
    }
}
