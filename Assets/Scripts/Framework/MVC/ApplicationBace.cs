using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class ApplicationBace<T> : Single<T>
    where T : MonoBehaviour
{
    //注册控制器

    protected void NewController(string eventName, Type type)
    {
        MVC.NewController(eventName, type);
    }

    //发送事件
    protected void SendEvent(string eventName, object arg = null)
    {
        MVC.SendEvent(eventName, arg);
    }

}
