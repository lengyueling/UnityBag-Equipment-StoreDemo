using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    void Start()
    {
        GetControl<Button>("Bag&Equip").onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<BagPanel>("BagPanel",E_UI_Layer.Mid);
            UIManager.Instance.ShowPanel<RolePanel>("RolePanel",E_UI_Layer.Mid);
        });

        GetControl<Button>("Shop").onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<ShopPanel>("ShopPanel", E_UI_Layer.Mid);
        });

        GetControl<Button>("AddMoney").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger("ChangeMoney", 100);
        });

        GetControl<Button>("AddGem").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger("ChangeGem", 100);
        });

        GetControl<Button>("AddPro").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger("ChangePro", 100);
        });
    }
    public override void ShowMe()
    {
        base.ShowMe();
        GetControl<Text>("Name").text = "名字：" + GameDataMgr.Instance.playerInfo.name;
        GetControl<Text>("Level").text = "等级：" + GameDataMgr.Instance.playerInfo.lev.ToString();
        GetControl<Text>("Gold").text = "金币：" + GameDataMgr.Instance.playerInfo.money.ToString();
        GetControl<Text>("Gem").text = "宝石：" + GameDataMgr.Instance.playerInfo.gem.ToString();
        GetControl<Text>("Pro").text = "能量：" + GameDataMgr.Instance.playerInfo.pro.ToString();
        EventCenter.Instance.AddEventListener<int>("ChangeMoney", UpdatePanel);
        EventCenter.Instance.AddEventListener<int>("ChangeGem", UpdatePanel);
        EventCenter.Instance.AddEventListener<int>("ChangePro", UpdatePanel);
    }

    private void UpdatePanel(int num)
    {
        GetControl<Text>("Gold").text = "金币：" + GameDataMgr.Instance.playerInfo.money.ToString();
        GetControl<Text>("Gem").text = "宝石：" + GameDataMgr.Instance.playerInfo.gem.ToString();
        GetControl<Text>("Pro").text = "能量：" + GameDataMgr.Instance.playerInfo.pro.ToString();
    }
    public override void HideMe()
    {
        base.HideMe();
        EventCenter.Instance.RemoveEventListener<int>("ChangeMoney", UpdatePanel);
        EventCenter.Instance.RemoveEventListener<int>("ChangeGem", UpdatePanel);
        EventCenter.Instance.RemoveEventListener<int>("ChangePro", UpdatePanel);
    }
}
