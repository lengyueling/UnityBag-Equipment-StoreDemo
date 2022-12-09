using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsMgr : Singleton<TipsMgr>
{
    public void ShowOneBtnTips(string info)
    {
        UIManager.Instance.ShowPanel<OneBtnTipsPanel>("OneBtnTipsPanel", E_UI_Layer.System, (panel) =>
        {
            panel.InitInfo(info);
        });
    }
}
