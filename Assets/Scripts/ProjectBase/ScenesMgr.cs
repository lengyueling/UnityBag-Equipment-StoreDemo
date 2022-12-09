using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesMgr : Singleton<ScenesMgr>
{
    public void LoadScene(string name, UnityAction fun)
    {
        SceneManager.LoadScene(name);
        fun();
    }
    public void LoadSceneAsny(string name, UnityAction fun)
    {
        MonoMgr.Instance.StartCoroutine(IELoadSceneAsync(name, fun));
    }
    IEnumerator IELoadSceneAsync(string name, UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while (!ao.isDone)
        {
            EventCenter.Instance.EventTrigger("ProgressUpdate", ao.progress);
            yield return ao.progress;
        }
        fun();
    }
}
