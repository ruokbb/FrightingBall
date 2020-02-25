using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIEnd : View
{

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    bool isOne = true;
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_End;
        }
    }
    #endregion

    #region 方法
    void Win()
    {
        if (!isOne) return;

        Show();

        //设置背景
        Sprite bgimg = Resources.Load("win",typeof(Sprite)) as Sprite;
        GetComponent<Image>().sprite = bgimg;

        //音效和按钮文字
        Game.instance.Sound.playEffect("Win", 1f);

        isOne = false;

    }

    void Lose()
    {
        if (!isOne) return;

        Show();

        //设置背景
        Sprite bgimg = Resources.Load("fail",typeof(Sprite)) as Sprite;
        GetComponent<Image>().sprite = bgimg;

        //音效和按钮文字

        Game.instance.Sound.playEffect("Lose", 1f);
        isOne = false;

    }

    public void Again()
    {
        if (!Game.instance.isServer)
            NetworkManager.singleton.StopClient();
        else
            NetworkManager.singleton.StopHost();
    }
    #endregion

    #region unity回调
    #endregion

    #region 事件回调

    public override void NewEvents()
    {
        this.AttentionEvents.Add(Consts.E_End);
    }

    public override void HandleEvent(string eventName, object data)
    {

        switch (eventName)
        {
            case Consts.E_End:

                    if (Game.instance.isFirst) break;

                    if (Game.instance.isWin)
                        Win();
                    else
                        Lose();
                break;
        }
    }
    #endregion

    #region 帮助方法
    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    #endregion


}
