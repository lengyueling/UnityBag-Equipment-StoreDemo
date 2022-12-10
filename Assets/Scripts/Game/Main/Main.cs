using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        GameDataMgr.Instance.Init();
        UIManager.Instance.ShowPanel<MainPanel>("MainPanel", E_UI_Layer.Bot);
        BagMgr.Instance.Init();
    }
}
