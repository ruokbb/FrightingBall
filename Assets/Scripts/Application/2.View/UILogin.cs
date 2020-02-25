using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UILogin : View
{

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_Login;
        }
    }
    #endregion

    #region 方法

    //设置network端口号
    void Setport()
    {
        //NetworkManager.singleton.StopHost();//断开连接
        NetworkManager.singleton.networkPort = 7777;
    }

    //创建房间
    public void CreatHost()
    {

        Setport();
        NetworkManager.singleton.StartHost();

        //播放音效
        Game.instance.Sound.playEffect("Button", 1f);
    }

    //加入房间
    public void CreatClint()
    {

        Setport();
        //获取IP地址
        string adress = GetComponentInChildren<InputField>().text;
        NetworkManager.singleton.networkAddress = adress;

        //播放音效
        Game.instance.Sound.playEffect("Button", 1f);

        NetworkManager.singleton.StartClient();//连接客户端
    }

    #endregion

    #region unity回调
    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {

    }
    #endregion

    #region 帮助方法
    #endregion




}
