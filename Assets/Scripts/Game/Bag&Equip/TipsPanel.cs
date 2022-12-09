using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{
    private ItemInfo itemInfo;
    public void InitInfo(ItemInfo info)
    {
        this.itemInfo = info;
        Item itemData = GameDataMgr.Instance.GetItemInfo(info.id);
        GetControl<Image>("Icon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + itemData.icon);
        GetControl<Text>("Num").text = "数量:" + info.num.ToString();
        GetControl<Text>("Name").text = itemData.name;
        GetControl<Text>("Tips").text = itemData.tips;
    }
}
