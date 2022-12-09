using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneBtnTipsPanel : BasePanel
{
    void Start()
    {
        GetControl<Button>("Close").onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("OneBtnTipsPanel");
        });
    }

    public void InitInfo(string text)
    {
        GetControl<Text>("Tips").text = text;
    }
}
