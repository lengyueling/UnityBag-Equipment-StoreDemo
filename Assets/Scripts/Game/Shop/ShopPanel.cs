using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel :BasePanel
{
    public Transform content;

    private List<ShopCell> cellList = new List<ShopCell>();

    void Start()
    {
        GetControl<Button>("Close").onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("ShopPanel");
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        foreach (var cell in cellList)
        {
            Destroy(cell.gameObject);
        }
        cellList.Clear();

        foreach (var item in GameDataMgr.Instance.shopInfos)
        {
            ResMgr.Instance.LoadAsync<GameObject>("UI/ShopCell", (go) =>
            {
                ShopCell cell = go.GetComponent<ShopCell>();
                cell.transform.SetParent(content);
                cell.transform.localScale = new Vector3(1, 1, 1);
                cell.InitInfo(item);
                cellList.Add(cell);
            });
        }
    }
}
