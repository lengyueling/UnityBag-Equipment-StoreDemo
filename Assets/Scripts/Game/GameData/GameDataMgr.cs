using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameDataMgr : Singleton<GameDataMgr>
{
    private Dictionary<int, Item> itemInfos = new Dictionary<int, Item>();
    private static string PlayerInfo_Url = Application.persistentDataPath + "/PlayerInfo.txt";
    public Player playerInfo;
    public List<ShopCellInfo> shopInfos;

    public void Init()
    {
        string itemInfo = ResMgr.Instance.Load<TextAsset>("Json/ItemInfo").text;
        Items items = JsonUtility.FromJson<Items>(itemInfo);
        foreach (var item in items.info)
        {
            itemInfos.Add(item.id, item);
        }

        if (File.Exists(PlayerInfo_Url))
        {
            byte[] bytes = File.ReadAllBytes(PlayerInfo_Url);
            string json = Encoding.UTF8.GetString(bytes);
            playerInfo = JsonUtility.FromJson<Player>(json);
        }
        else
        {
            playerInfo = new Player();
            SavePlayerInfo();
        }

        string shopInfo = ResMgr.Instance.Load<TextAsset>("Json/ShopInfo").text;
        Shops shops = JsonUtility.FromJson<Shops>(shopInfo);
        shopInfos = shops.info;
        EventCenter.Instance.AddEventListener<int>("ChangeMoney", ChangeMoney);
        EventCenter.Instance.AddEventListener<int>("ChangeGem", ChangeGem);
        EventCenter.Instance.AddEventListener<int>("ChangePro", ChangePro);
    }

    private void ChangeMoney(int num)
    {
        playerInfo.ChangeMoney(num);
        SavePlayerInfo();
    }

    private void ChangeGem(int num)
    {
        playerInfo.ChangeGem(num);
        SavePlayerInfo();
    }

    private void ChangePro(int num)
    {
        playerInfo.ChangePro(num);
        SavePlayerInfo();
    }

    public void SavePlayerInfo()
    {
        string json = JsonUtility.ToJson(playerInfo);
        File.WriteAllBytes(PlayerInfo_Url, Encoding.UTF8.GetBytes(json));
    }

    public Item GetItemInfo(int id)
    {
        if (itemInfos.ContainsKey(id))
        {
            return itemInfos[id];
        }
        return null;
    }
}

public class Player
{
    public string name;
    public int lev;
    public int money;
    public int gem;
    public int pro;
    public List<ItemInfo> items;
    public List<ItemInfo> equips;
    public List<ItemInfo> gems;
    public List<ItemInfo> nowEquips;
    public Player()
    {
        name = "xiaoming";
        lev = 10;
        money = 9999;
        gem = 999;
        pro = 99;
        items = new List<ItemInfo>() { new ItemInfo() { id = 3, num = 10 }, new ItemInfo() { id = 4, num = 5 } };
        equips = new List<ItemInfo>() { new ItemInfo() { id = 1, num = 1 }, new ItemInfo() { id = 2, num = 1 }, new ItemInfo() { id = 1, num = 1 }, new ItemInfo() { id = 7, num = 1 } };
        gems = new List<ItemInfo>() { new ItemInfo() { id = 5, num = 2 }, new ItemInfo() { id = 6, num = 30 } };
        nowEquips = new List<ItemInfo>();
    }
    public void AddItem(ItemInfo info)
    {
        Item item = GameDataMgr.Instance.GetItemInfo(info.id);
        switch (item.type)
        {
            case (int)E_Bag_Type.Item:
                bool isAdd = false;
                foreach (var it in items)
                {
                    if (it.id == info.id && it.num < 99)
                    {
                        if (it.num + info.num <= 99)
                        {
                            it.num += info.num;
                            isAdd = true;
                            break;
                        }
                        else
                        {
                            items.Add(new ItemInfo { id = info.id, num = info.num - (99 - it.num) });
                            it.num = 99;
                            isAdd = true;
                            break;
                        }
                    }
                }
                if (!isAdd)
                {
                    items.Add(info);
                }
                break;
            case (int)E_Bag_Type.Equip:
                equips.Add(info);
                break;
            case (int)E_Bag_Type.Gem:
                isAdd = false;
                foreach (var it in gems)
                {
                    if (it.id == info.id && it.num < 99)
                    {
                        if (it.num + info.num <= 99)
                        {
                            it.num += info.num;
                            isAdd = true;
                            break;
                        }
                        else
                        {
                            gems.Add(new ItemInfo { id = info.id, num = info.num - (99 - it.num) });
                            it.num = 99;
                            isAdd = true;
                            break;
                        }
                    }
                }
                if (!isAdd)
                {
                    gems.Add(info);
                }
                break;
        }
    }

    public void ChangeMoney(int num)
    {
        if (this.money <0 && (this.money < num || num > 0))
        {
            return;
        }
        this.money += num;
    }

    public void ChangeGem(int num)
    {
        if (this.gem < 0 && (this.gem < num || num > 0))
        {
            return;
        }
        this.gem += num;
    }

    public void ChangePro(int num)
    {
        if (this.pro < 0 && (this.pro < num || num > 0))
        {
            return;
        }
        this.pro += num;
    }
}

[System.Serializable]
public class ItemInfo
{
    public int id;
    public int num;
}

public class Items
{
    public List<Item> info;
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string icon;
    public int equipType;
    public int type;
    public int price;
    public string tips;
}

public class Shops
{
    public List<ShopCellInfo> info;
}

[System.Serializable]
public class ShopCellInfo
{
    public int id;
    public ItemInfo itemInfo;
    public int priceType;
    public int price;
    public string tips;
}
