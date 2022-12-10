using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : BasePanel
{
    private ShopCellInfo info;
    void Start()
    {
        GetControl<Button>("Buy").onClick.AddListener(() =>
        {
            if (info.priceType == 1 && GameDataMgr.Instance.playerInfo.money >= info.price)
            {
                GameDataMgr.Instance.playerInfo.AddItem(info.itemInfo);
                EventCenter.Instance.EventTrigger("ChangeMoney", -info.price);
                TipsMgr.Instance.ShowOneBtnTips("购买成功");
            }
            else if(info.priceType == 2 && GameDataMgr.Instance.playerInfo.gem >= info.price)
            {
                GameDataMgr.Instance.playerInfo.AddItem(info.itemInfo);
                EventCenter.Instance.EventTrigger("ChangeGem", -info.price);
                TipsMgr.Instance.ShowOneBtnTips("购买成功");
            }
            else
            {
                TipsMgr.Instance.ShowOneBtnTips("货币不足");
            }
        });
    }

    public void InitInfo(ShopCellInfo info)
    {
        this.info = info;
        Item item = GameDataMgr.Instance.GetItemInfo(info.itemInfo.id);
        GetControl<Image>("Icon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + item.icon);
        GetControl<Text>("Num").text = info.itemInfo.num.ToString();
        GetControl<Text>("Name").text = item.name;
        GetControl<Image>("Type").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + (info.priceType == 1 ? "gold" : "gem"));
        GetControl<Text>("Price").text = info.price.ToString();
        GetControl<Text>("Tips").text = info.tips;
    }
}
