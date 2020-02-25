using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class View : MonoBehaviour
{
    //视图标识
    public abstract string Name { get; }

    //处理事件
    public abstract void HandleEvent(string eventName, object data);

    //视图关心的函数
    [HideInInspector]
    public List<string> AttentionEvents = new List<string>();

    //注册事件
    public virtual void NewEvents()
    {

    }
    //获取模型
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    //发送事件给控制器
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
