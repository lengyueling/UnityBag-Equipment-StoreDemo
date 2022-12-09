using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolData
{
    public GameObject baseObj;
    public List<GameObject> poolList;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        baseObj = new GameObject(obj.name);
        baseObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>(){};
        PushObj(obj);
    }

    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        obj.transform.parent = baseObj.transform;
        obj.SetActive(false);
    }

    public GameObject GetObj()
    {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
}

public class PoolMgr : Singleton<PoolMgr>
{
    private GameObject poolObj;
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    public void GetPool(string name, UnityAction<GameObject> callback)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            callback(poolDic[name].GetObj());
}
        else
        {
            ResMgr.Instance.LoadAsync<GameObject>(name, (obj) => 
            {
                obj.name = name;
                callback(obj);
            });
        }
    }

    public void PushObj(string name, GameObject obj)
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }
        obj.transform.parent = poolObj.transform;

        obj.SetActive(false);
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            poolDic.Add(name, new PoolData(obj, poolObj));
        }
    }

    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
