using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Controller
{
    //获取模型和视图 
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    protected T GetView<T>()
       where T : View
    {
        return MVC.GetView<T>() as T;
    }

    //新建模型，视图，控制器
    protected void NewModel(Model modle)
    {
        MVC.NewModel(modle);
    }

    protected void NewView(View view)
    {
        MVC.NewView(view);
    }

    protected void NewController(string eventName, Type type)
    {
        MVC.NewController(eventName, type);
    }

    //处理系统消息
    public abstract void Execute(object data);
}

