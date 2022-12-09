using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePanel : BasePanel
{
    public ItemCell itemHead;
    public ItemCell itemNeck;
    public ItemCell ItemWeapon;
    public ItemCell ItemCloth;
    public ItemCell ItemTrousers;
    public ItemCell ItemShose;

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "Close":
                UIManager.Instance.HidePanel("RolePanel");
                break;
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdateRolePanel();
    }

    public void UpdateRolePanel()
    {
        List<ItemInfo> nowEquips = GameDataMgr.Instance.playerInfo.nowEquips;
        Item itemInfo;
        for (int i = 0; i < nowEquips.Count; i++)
        {
            itemInfo = GameDataMgr.Instance.GetItemInfo(nowEquips[i].id);
            switch (itemInfo.equipType)
            {
                case (int)E_Item_Type.Head:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Neck:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Weapon:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Cloth:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Trousers:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Shoes:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
            }
        }
    }
}
