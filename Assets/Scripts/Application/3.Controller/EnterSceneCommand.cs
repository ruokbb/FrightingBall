using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class EnterSceneCommand : Controller
{
    public override void Execute(object data)
    {
        SceneArgs sceneArgs = data as SceneArgs;

        switch (sceneArgs.Level)
        {
            case 0:
                break;

            case 1:
                NewView(GameObject.Find("UILogin").GetComponent<UILogin>());
                Game.instance.Sound.playBg("Login");
                break;
            case 2:
                NewView(GameObject.Find("UIEasyTouch").GetComponent<UIEasyTouch>());
                NewView(GameObject.Find("UIInfo").GetComponent<UIInfo>());
                NewView(GameObject.Find("UIInfo").transform.Find("UICountDown").GetComponent<UICountDown>());
                NewView(GameObject.Find("UIInfo").transform.Find("UIEnd").GetComponent<UIEnd>());
                Game.instance.Sound.playBg("Game");
                break;
        }

    }
}
