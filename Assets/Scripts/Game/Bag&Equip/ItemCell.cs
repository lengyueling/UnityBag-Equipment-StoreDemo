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
    private ItemInfo _itemInfo;
    public ItemInfo itemInfo
    {
        get
        {
            return _itemInfo;
        }
    }
    private bool isDragOpen = false;

    public E_Item_Type equipType = E_Item_Type.Bag;

    public void InitInfo(ItemInfo info)
    {
        this._itemInfo = info;
        if (info == null)
        {
            GetControl<Image>("Icon").color = new Color(0, 0, 0, 0);
            return;
        }
        GetControl<Image>("Icon").color = new Color(1, 1, 1, 1);
        Item itemData = GameDataMgr.Instance.GetItemInfo(info.id);
        GetControl<Image>("Icon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + itemData.icon);
        if (equipType == E_Item_Type.Bag)
        {
            GetControl<Text>("Num").text = info.num.ToString();
        }

        if (itemData.type == (int)E_Bag_Type.Equip && isDragOpen == false)
        {
            isDragOpen = true;
            OpenDragEvent();
        }
    }
    protected override void Awake()
    {
        base.Awake();
        GetControl<Image>("Icon").color = new Color(0, 0, 0, 0);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.PointerEnter, EnterItemCell);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.PointerExit, ExitItemCell);
    }

    private void OpenDragEvent()
    {
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.BeginDrag, BeginDragItemCell);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.Drag, DragItemCell);
        UIManager.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.EndDrag, EndDragItemCell);
    }

    private void EnterItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger("ItemCellEnter", this);
    }
    private void ExitItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger("ItemCellExit", this);
    }

    private void BeginDragItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger("ItemCellBeginDrag", this);
    }
    private void DragItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger("ItemCellDrag", date);
    }

    private void EndDragItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger("ItemCellEndDrag", this);
    }
}
