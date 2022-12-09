using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum E_Item_Type
{
    Bag = 0,
    Head,
    Neck,
    Weapon,
    Cloth,
    Trousers,
    Shoes,
}

public class ItemCell : BasePanel
{
    private ItemInfo itemInfo;
    public E_Item_Type equipType = E_Item_Type.Bag;

    public void InitInfo(ItemInfo info)
    {
        this.itemInfo = info;
        GetControl<Image>("Icon").color = new Color(1, 1, 1, 1);
        Item itemData = GameDataMgr.Instance.GetItemInfo(info.id);
        GetControl<Image>("Icon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + itemData.icon);
        GetControl<Text>("Num").text = info.num.ToString();
    }
    protected override void Awake()
    {
        base.Awake();
        GetControl<Image>("Icon").color = new Color(0, 0, 0, 0);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.PointerEnter, EnterItemCell);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.PointerExit, ExitItemCell);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.BeginDrag, BeginDragItemCell);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.Drag, DragItemCell);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.EndDrag, EndDragItemCell);
    }

    private void EnterItemCell(BaseEventData date)
    {
        if (itemInfo == null)
        {
            return;
        }
        UIManager.Instance.ShowPanel<TipsPanel>("TipsPanel", E_UI_Layer.Top, (panel) =>
        {
            panel.InitInfo(itemInfo);
            panel.transform.position = GetControl<Image>("Icon").transform.position;
        });
    }
    private void ExitItemCell(BaseEventData date)
    {
        if (itemInfo == null)
        {
            return;
        }
        UIManager.Instance.HidePanel("TipsPanel");
    }

    private void BeginDragItemCell(BaseEventData date)
    {

    }
    private void DragItemCell(BaseEventData date)
    {

    }

    private void EndDragItemCell(BaseEventData date)
    {

    }
}
