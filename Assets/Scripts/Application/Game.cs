using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]

public class Game : ApplicationBace<Game>
{


    //全局
    public Sound Sound;//声音管理
    public StaticData StaticData;

    //记录当前胜负和是否是服务器
    public bool isServer;
    public bool isWin;
    public bool isFirst = true;

    //加载场景发送消息给MVC
    public void LoadScene(int Level)
    {
        //获取事件参数
        SceneArgs e = new SceneArgs();
        e.Level = SceneManager.GetActiveScene().buildIndex;

        //退出当前场景发布事件
        SendEvent(Consts.E_ExitScene, e);

        //加载新场景
        SceneManager.LoadScene(Level, LoadSceneMode.Single);
    }

    //场景加载时发送消息给MVC
    private void OnLevelWasLoaded(int level)
    {
        //构建事件
        SceneArgs e = new SceneArgs();
        e.Level = level;

        SendEvent(Consts.E_EnterScene, e);
    }







    private void Start()
    {

        //跳转场景保存
        Object.DontDestroyOnLoad(this.gameObject);

        Sound = Sound.instance;
        StaticData = StaticData.instance;

        NewController(Consts.E_StartUp, typeof(StartUpCommand));//注册控制器

        //启动游戏 
        SendEvent(Consts.E_StartUp);
    }
}
