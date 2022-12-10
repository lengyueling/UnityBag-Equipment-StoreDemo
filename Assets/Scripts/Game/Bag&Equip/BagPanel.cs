using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_Bag_Type
{
    Item = 1,
    Equip,
    Gem
}

public class BagPanel : BasePanel
{
    public Transform content;
    private List<ItemCell> celllist = new List<ItemCell>();
    void Start()
    {
        GetControl<Button>("Close").onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("BagPanel");

        });
        GetControl<Toggle>("Item").onValueChanged.AddListener((value) => 
        {
            if (value)
            {
                ChangeType(E_Bag_Type.Item);
            }
        });
        GetControl<Toggle>("Equip").onValueChanged.AddListener((value) => 
        {
            if (value)
            {
                ChangeType(E_Bag_Type.Equip);
            }
        });
        GetControl<Toggle>("Gem").onValueChanged.AddListener((value) => 
        {
            if (value)
            {
                ChangeType(E_Bag_Type.Gem);
            }
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        ChangeType(E_Bag_Type.Item);
    }

    public void ChangeType(E_Bag_Type type)
    {
        List<ItemInfo> info = GameDataMgr.Instance.playerInfo.items;
        switch (type)
        {
            case E_Bag_Type.Item:
                info = GameDataMgr.Instance.playerInfo.items;
                break;
            case E_Bag_Type.Equip:
                info = GameDataMgr.Instance.playerInfo.equips;
                break;
            case E_Bag_Type.Gem:
                info = GameDataMgr.Instance.playerInfo.gems;
                break;
        }
        foreach (var item in celllist)
        {
            Destroy(item.gameObject);
        }
        celllist.Clear();
        foreach (var item in info)
        {
            ResMgr.Instance.LoadAsync<GameObject>("UI/ItemCell", (go) =>
            {
                ItemCell cell = go.GetComponent<ItemCell>();
                cell.transform.SetParent(content);
                cell.transform.localScale = new Vector3(1, 1, 1);
                cell.InitInfo(item);
                celllist.Add(cell);
            });
        }
    }
}
