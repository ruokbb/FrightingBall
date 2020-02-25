using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class MVC
{
    //存储
    public static Dictionary<string, Model> models = new Dictionary<string, Model>();//名字---模型
    public static Dictionary<string, View> views = new Dictionary<string, View>(); //名字---视图
    public static Dictionary<string, Type> commandMaps = new Dictionary<string, Type>(); //事件名字---控制器类型



    //新建
    public static void NewModel(Model modle)
    {
        models[modle.Name] = modle;
    }

    public static void NewView(View view)
    {
        if (views.ContainsKey(view.Name)) //重复的删掉原来的
            views.Remove(view.Name);

        view.NewEvents();
        views[view.Name] = view;
    }

    public static void NewController(string name, Type type)
    {
        commandMaps[name] = type;
    }

    //查询获取

    public static T GetModel<T>()
        where T : Model
    {
        foreach (Model mod in models.Values)
        {
            if (mod is T)
                return (T)mod;
        }
        return null;
    }

    public static T GetView<T>()
        where T : View
    {
        foreach (View view in views.Values)
        {
            if (view is T)
                return (T)view;
        }
        return null;
    }


    //分发事件
    public static void SendEvent(string eventName, object data = null)
    {
        //控制器响应
        if (commandMaps.ContainsKey(eventName))
        {
            Type t = commandMaps[eventName];
            Controller controller = Activator.CreateInstance(t) as Controller;//用反射机制通过类型获取对应的控制器
            controller.Execute(data);//调用控制器的处理事件方法
        }

        //视图相应

        foreach (View v in views.Values)
        {
            if (v.AttentionEvents.Contains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }
    }

}

