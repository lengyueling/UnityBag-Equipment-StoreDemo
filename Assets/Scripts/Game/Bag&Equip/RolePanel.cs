using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePanel : BasePanel
{
    public ItemCell itemHead;
    public ItemCell itemNeck;
    public ItemCell itemWeapon;
    public ItemCell itemCloth;
    public ItemCell itemTrousers;
    public ItemCell itemShose;

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
        itemHead.InitInfo(null);
        itemNeck.InitInfo(null);
        itemWeapon.InitInfo(null);
        itemCloth.InitInfo(null);
        itemTrousers.InitInfo(null);
        itemShose.InitInfo(null);
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
                    itemNeck.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Weapon:
                    itemWeapon.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Cloth:
                    itemCloth.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Trousers:
                    itemTrousers.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Shoes:
                    itemShose.InitInfo(nowEquips[i]);
                    break;
            }
        }
    }
}
