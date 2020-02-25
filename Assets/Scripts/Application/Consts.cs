using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts {

    //目录
    public static string LevelDir = Application.dataPath + @"\Res\Levels";
    public static string MapDir = Application.dataPath + @"\Res\Maps";//图片的目录
    public static string CardDir = Application.dataPath + @"\Res\Cards";
    public static string PrefabDir = Application.dataPath + @"\Resources\Prefabs";

    //参数
    public const string GameProgress = "GameProgress";
    public const float DotClosedDistance = 0.1f;
    public const float RangeClosedDistance = 0.7f;


    //模型
    public const string M_PlayerModel = "M_PlayerModel";
    public const string M_RoundModel = " M_RoundModel";

    //事件
    public const string E_EnterScene = "E_EnterScene"; //SceneArgs
    public const string E_ExitScene = "E_ExitScene";//SceneArgs
    public const string E_StartUp = "E_StartUp"; //游戏的启动
    public const string E_Jump = "E_Jump"; //跳跃
    public const string E_CountDownStart = "E_CountDownStart"; //开始倒计时
    public const string E_CountDownComplete = "E_CountDownComplete"; //倒计时结束
    public const string E_End = "E_End"; //退出关卡

    public const string E_SpawnTower = "E_SpawnTower";//SpawnTowerArgs
    public const string E_UpgradeTower = "E_UpgradeTower";//UpgradeTowerArgs
    public const string E_SellTower = "E_SellTower";//SellTowerArgs
    public const string E_ShowCreate = "E_ShowCreate";//ShowCreatorArgs
    public const string E_ShowUpgrade = "E_ShowUpgrade";//ShowUpgradeArgs
    public const string E_HidePopup = "E_HidePopup";




    //视图
    public const string V_Login = "V_Login";//登陆的视图
    public const string V_EasyTouch = "V_EasyTouch";//触控的视图
    public const string V_BG = "V_BG";//背景的视图
    public const string V_Info = "V_Info";//各类信息视图
    public const string V_CountDown = "V_CountDown";//倒计时视图
    public const string V_End = "V_End";//结束
    public const string V_Wall = "V_Wall";//空气墙

    public const string V_Win = "V_Win";//胜利
    public const string V_Lost = "V_Lost";//失败
    public const string V_Sytem = "V_Sytem";//系统
    public const string V_Complete = "V_Complete";
    public const string V_Spanwner = "V_Spanwner";
    public const string V_TowerPopup = "V_TowerPopup";
}
