using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();
    }

    public virtual void ShowMe()
    {

    }

    public virtual void HideMe()
    {

    }

    protected virtual void OnClick(string btnName)
    {

    }

    protected virtual void OnValueChanged(string toggleName, bool value)
    {

    }

    public T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            foreach (var item in controlDic[controlName])
            {
                if (item is T)
                {
                    return item as T;
                }
            }
        }
        return null;
    }

    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        foreach (var item in controls)
        {
            if (controlDic.ContainsKey(item.gameObject.name))
            {
                controlDic[item.gameObject.name].Add(item);
            }
            else
            {
                controlDic.Add(item.gameObject.name, new List<UIBehaviour>() { item });
            }

            if (item is Button)
            {
                (item as Button).onClick.AddListener(()=> 
                {
                    OnClick(item.gameObject.name);
                });
            }
            else if (item is Toggle)
            {
                (item as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(item.gameObject.name, value);
                });
            }
        }
    }


}

