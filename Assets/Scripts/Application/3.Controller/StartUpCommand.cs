using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : Controller
{
    //MVC注册入口
    public override void Execute(object data)
    {
        //注册模型
        NewModel(new PlayerModel());


        //注册控制器
        NewController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        NewController(Consts.E_ExitScene, typeof(ExitSceneCommand));
        NewController(Consts.E_Jump, typeof(JumpCommand));
        NewController(Consts.E_CountDownStart, typeof(CountDownStartCommand));
 

        //进入开始场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
