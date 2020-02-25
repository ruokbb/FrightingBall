using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;

public class UIInfo : View
{

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Slider ServeSlider;
    public Slider ClintSlider;
    private float serveHp = 100;
    private float clintHp = 100;

    //获取IP地址
    public Text ipText;
    private IPHostEntry iPHostEntry;
    private string ipAddress = null;

    //确定服务器角色和本地角色
    GameObject[] Players;
    GameObject ServerPlayer;
    GameObject ClientPlayer;
    Vector3 SpawnFirst;
    Vector3 SpawnSecond;
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_Info;
        }
    }

    public float ServeHp
    {
        get
        {
            return serveHp;
        }

        set
        {
            ServeSlider.value = value / serveHp;   
        }
    }

    public float ClintHp
    {
        get
        {
            return clintHp;
        }

        set
        {
            ClintSlider.value = value / clintHp;
        }
    }

    #endregion

    #region 方法

    public void DisplayIP()
    {
        ipText.gameObject.SetActive(true);
        iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
        foreach(IPAddress ip in iPHostEntry.AddressList)
        {
            if(ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = ip.ToString();
            }
        }
        ipText.text = ipAddress;
    }

    /// <summary>
    /// 开始游戏，关闭IP显示，发送倒计时事件
    /// </summary>
    public void StartGame()
    {
        ipText.gameObject.SetActive(false);

        //确定两个角色,绑定事件
        FindPlayer();
        CountDownStartArgs arg = new CountDownStartArgs {
            ServerPlayer = ServerPlayer,
            ClientPlayer = ClientPlayer,
            ServerPos = SpawnFirst,
            ClientPos = SpawnSecond,
        };
        SendEvent(Consts.E_CountDownStart,arg);
    }

    /// <summary>
    /// 死亡,who=0:服务器死亡，who=1:客户端死亡
    /// </summary>
    public void Dead(int who)
    {
        //记录是否胜利
        if (Game.instance.isServer)
        {
            if (who == 0)
                Game.instance.isWin = false;
            else
                Game.instance.isWin = true;
        }
        else
        {
            if (who == 0)
                Game.instance.isWin = true;
            else
                Game.instance.isWin = false;
        }

        Game.instance.isFirst = false;

        SendEvent(Consts.E_End);

    } 

    /// <summary>
    /// 创建进场特效
    /// </summary>
    void SetEnterEffect()
    {
        GameObject red = Resources.Load("Prefabs/Spe/AuraDisableFire") as GameObject;
        Vector3 pos = new Vector3(SpawnFirst.x, 0.8f, SpawnFirst.z);
        Instantiate(red, pos, Quaternion.identity);

        GameObject blue = Resources.Load("Prefabs/Spe/AuraDisableFrost") as GameObject;
        Vector3 pos1 = new Vector3(SpawnSecond.x, 0.8f, SpawnSecond.z);
        Instantiate(blue, pos1, Quaternion.identity);
    }
    #endregion

    #region unity回调
    private void Start()
    {
        ServeHp = 100;
        ClintHp = 100;
    }
    #endregion

    #region 事件回调

    public override void NewEvents()
    {
        this.AttentionEvents.Add(Consts.E_CountDownComplete);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_CountDownComplete :

                //创建进场特效
                SetEnterEffect();

                break;
        }
    }
    #endregion

    #region 帮助方法

    void FindPlayer()
    {
        //找出Player
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject i in Players)
        {
            if (i.GetComponent<Player>().isLocalPlayer)
            {
                if (i.GetComponent<Player>().isServer)
                    ServerPlayer = i;
                else
                    ClientPlayer = i;
            }
            else
            {
                if (i.GetComponent<Player>().isServer)
                    ClientPlayer = i;
                else
                    ServerPlayer = i;
            }
        }

        //确定初始位置
        SpawnFirst = GameObject.FindGameObjectWithTag("SpawnFirst").transform.position;
        SpawnSecond = GameObject.FindGameObjectWithTag("SpawnSecond").transform.position;
    }

    #endregion


}
