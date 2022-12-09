using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : Singleton<ResMgr>
{
    public T Load<T>(string name) where T :Object
    {

        T res = Resources.Load<T>(name);
        if (res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        else
        {
            return res;
        }
    }

    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        MonoMgr.Instance.StartCoroutine(IELoadAsync(name, callback));
    }

    IEnumerator IELoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest req = Resources.LoadAsync<T>(name);
        yield return req;
        if (req.asset is GameObject)
        {
            callback(GameObject.Instantiate(req.asset) as T);
        }
        else
        {
            callback(req.asset as T);
        }
    }
}
