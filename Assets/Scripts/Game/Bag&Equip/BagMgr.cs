using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagMgr : Singleton<BagMgr>
{
    private ItemCell nowDragItem;
    private ItemCell nowInItem;
    private Image nowSelItemImg;

    private bool isDraging = false;

    public void Init()
    {
        EventCenter.Instance.AddEventListener<ItemCell>("ItemCellBeginDrag", BeginDragItemCell);
        EventCenter.Instance.AddEventListener<BaseEventData>("ItemCellDrag", DragItemCell);
        EventCenter.Instance.AddEventListener<ItemCell>("ItemCellEndDrag", EndDragItemCell);
        EventCenter.Instance.AddEventListener<ItemCell>("ItemCellEnter", EnterItemCell);
        EventCenter.Instance.AddEventListener<ItemCell>("ItemCellExit", ExitItemCell);
    }

    public void ChangeEquip()
    {
        //当前被拖动的格子是从背包拖到装备的
        if (nowDragItem.equipType == E_Item_Type.Bag)
        {
            //存在拖入的格子且不拖入进的格子不是背包中的格子
            if (nowInItem != null && nowInItem.equipType != E_Item_Type.Bag)
            {
                Item info = GameDataMgr.Instance.GetItemInfo(nowDragItem.itemInfo.id);
                //判断拖入的格子类型和装备类型是否一致
                if ((int)nowInItem.equipType == info.equipType)
                {
                    //判断当前拖入的格子是否是空的，是空的则直接装备，如果不是空的需要与背包拖入的装备交换
                    if (nowInItem.itemInfo == null)
                    {
                        GameDataMgr.Instance.playerInfo.nowEquips.Add(nowDragItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.equips.Remove(nowDragItem.itemInfo);
                    }
                    else
                    {
                        GameDataMgr.Instance.playerInfo.nowEquips.Remove(nowInItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.nowEquips.Add(nowDragItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.equips.Remove(nowDragItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.equips.Add(nowInItem.itemInfo);
                    }
                    UIManager.Instance.GetPanel<BagPanel>("BagPanel").ChangeType(E_Bag_Type.Equip);
                    UIManager.Instance.GetPanel<RolePanel>("RolePanel").UpdateRolePanel();
                    GameDataMgr.Instance.SavePlayerInfo();
                }
            }
        }
        else
        {
            //没有拖入到任何格子中，下装备
            if (nowInItem == null || nowInItem.equipType != E_Item_Type.Bag)
            {
                GameDataMgr.Instance.playerInfo.nowEquips.Remove(nowDragItem.itemInfo);
                GameDataMgr.Instance.playerInfo.equips.Add(nowDragItem.itemInfo);
            }
            //交换装备
            else if (nowInItem != null && nowInItem.equipType == E_Item_Type.Bag)
            {
                GameDataMgr.Instance.playerInfo.nowEquips.Remove(nowDragItem.itemInfo);
                GameDataMgr.Instance.playerInfo.nowEquips.Add(nowInItem.itemInfo);
                GameDataMgr.Instance.playerInfo.equips.Remove(nowInItem.itemInfo);
                GameDataMgr.Instance.playerInfo.equips.Add(nowDragItem.itemInfo);
            }
            UIManager.Instance.GetPanel<BagPanel>("BagPanel").ChangeType(E_Bag_Type.Equip);
            UIManager.Instance.GetPanel<RolePanel>("RolePanel").UpdateRolePanel();
            GameDataMgr.Instance.SavePlayerInfo();
        }
    }

    private void BeginDragItemCell(ItemCell itemCell)
    {
        if (itemCell.itemInfo == null)
        {
            return;
        }
        isDraging = true;
        UIManager.Instance.HidePanel("TipsPanel");
        nowDragItem = itemCell;
        PoolMgr.Instance.GetPool("UI/Icon", (obj) =>
         {
             nowSelItemImg = obj.GetComponent<Image>();
             nowSelItemImg.sprite = itemCell.GetControl<Image>("Icon").sprite;
             nowSelItemImg.transform.SetParent(UIManager.Instance.canvas.Find("Top"));
             nowSelItemImg.transform.localScale = Vector3.one;
             if (!isDraging)
             {
                 PoolMgr.Instance.PushObj(nowSelItemImg.name, nowSelItemImg.gameObject);
                 nowSelItemImg = null;
             }
         });
    }

    private void DragItemCell(BaseEventData data)
    {
        if (nowSelItemImg == null)
        {
            return;
        }
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.canvas.Find("Top") as RectTransform, (data as PointerEventData).position, (data as PointerEventData).pressEventCamera, out localPos);
        nowSelItemImg.transform.localPosition = localPos;
    }

    private void EndDragItemCell(ItemCell itemCell)
    {
        isDraging = false;
        ChangeEquip();
        nowDragItem = null;
        nowInItem = null;
        if (nowSelItemImg == null)
        {
            return;
        }
        PoolMgr.Instance.PushObj(nowSelItemImg.name, nowSelItemImg.gameObject);
        nowSelItemImg = null;
    }

    private void EnterItemCell(ItemCell itemCell)
    {
        Debug.LogFormat("Enter");
        if (isDraging)
        {
            nowInItem = itemCell;
            return;
        }
        if (itemCell.itemInfo == null)
        {
            return;
        }
        UIManager.Instance.ShowPanel<TipsPanel>("TipsPanel", E_UI_Layer.Top, (panel) =>
        {
            panel.InitInfo(itemCell.itemInfo);
            panel.transform.position = itemCell.GetControl<Image>("Icon").transform.position;
            if (isDraging)
            {
                UIManager.Instance.HidePanel("TipsPanel");
            }
        });
    }

    private void ExitItemCell(ItemCell itemCell)
    {
        Debug.LogFormat("Exit");
        if (isDraging)
        {
            nowInItem = null;
            return;
        }
        if (itemCell.itemInfo == null)
        {
            return;
        }
        UIManager.Instance.HidePanel("TipsPanel");
    }
}
