using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIEasyTouch : View
{

    #region 常量
    private const float rate = 0.3f;
    #endregion

    #region 事件
    #endregion

    #region 字段
    //确定本地角色
    GameObject[] Players;
    GameObject LocalPlayer;
    GameObject OtherPlayer;
    bool fundLocalPlayer = false;
    bool fundOtherPlayer = false;

    PlayerModel playerModel;

    public float time = 0;

    List<Transform> all = new List<Transform>();//全部子对象

    //技能相关
    bool skillWallFinish = true;
    public GameObject skillWallCountDown;

    bool skillShootFinish = true;
    public GameObject skillShootCountDown;

    bool skillPitchFireFinish = true;
    public GameObject skillPitchFireCountDown;
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_EasyTouch;
        }
    }
    #endregion

    #region 方法
    //控制主角移动
    public void Move(Vector2 a)
    {
        FindLocalPlayer();//确定LocalPlayer

        
        GameObject camera = GameObject.FindGameObjectWithTag("Camera");

        Vector3  targetDirection = new Vector3(a.x,0,a.y);
        float y = camera.transform.rotation.eulerAngles.y;
        targetDirection = Quaternion.Euler(0, y, 0) * targetDirection;

        //根据摄像机角度和轮盘角度进行移动
        LocalPlayer.transform.Translate(targetDirection * Time.deltaTime * playerModel.moveSpeed, Space.World);
    }

    /// <summary>
    /// 转视角
    /// </summary>
    /// <param name="a"></param>
    public void Shoot(Vector2 a)
    {
        FindLocalPlayer();//确定LocalPlayer

        //调整射击方向
        GameObject camera = GameObject.FindGameObjectWithTag("Camera");
        Vector3 targetDirection = new Vector3(a.x, 0, a.y);
        float y = camera.transform.rotation.eulerAngles.y;
        targetDirection = Quaternion.Euler(0, y, 0) * targetDirection;

        LocalPlayer.transform.rotation = Quaternion.LookRotation(targetDirection);

    }

    //射击，调用Player的方法，在服务器运行
    public void Fire()
    {

        time += Time.deltaTime;
        if (time > rate)
        {
            time = 0;
        }
        else
        {
            return;
        }

        //播放音效
        Game.instance.Sound.playEffect("Shoot",0.4f);

        LocalPlayer.GetComponent<Player>().CmdFire();
    }

    //相机跟随
    void CameraFollow()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("Camera");
        camera.transform.position = LocalPlayer.transform.position;
    }

    /// <summary>
    /// 倒计时停止操作
    /// </summary>
    void StopAllTouch()
    {
        foreach(Transform a in transform)
        {
            all.Add(a);
            a.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 倒计时完毕，开始战斗
    /// </summary>
    void StartAllTouch()
    {
        foreach (Transform a in all)
        {
            a.gameObject.SetActive(true);
        }

        //播放开始游戏音效
        Game.instance.Sound.playEffect("GameStart", 0.6f);
    }

    /// <summary>
    /// 释放技能，建墙
    /// </summary>
    public void SkillSetWall()
    {
        if (skillWallFinish)
        {
            Game.instance.Sound.playEffect("SkillWall",0.7f);
            LocalPlayer.GetComponent<Player>().CmdSetWall();
            skillWallFinish = false;
            StartCoroutine(SetSkillCountDown(skillWallCountDown, 1,2));
        }
    }

    /// <summary>
    /// 释放技能，穿甲弹
    /// </summary>
    public void SkillShootBullet()
    {
        if (skillShootFinish)
        {
            Game.instance.Sound.playEffect("SkillShoot", 0.7f);
            LocalPlayer.GetComponent<Player>().CmdShootBullet();
            skillShootFinish = false;
            StartCoroutine(SetSkillCountDown(skillShootCountDown, 2, 3));
        }
    }

    /// <summary>
    /// 释放技能，燃烧瓶
    /// </summary>
    public void SkillPitchFire()
    {
        FindLocalPlayer();//确定LocalPlayer

        if (skillPitchFireFinish)
        {
            Game.instance.Sound.playEffect("SkillPitchFire", 0.7f);
            Vector3 pos = new Vector3(OtherPlayer.transform.position.x, 0.8f, OtherPlayer.transform.position.z);
            LocalPlayer.GetComponent<Player>().CmdPitchFire(pos);
            skillPitchFireFinish = false;
            StartCoroutine(SetSkillCountDown(skillPitchFireCountDown, 3, 5));
        }
    }

    #endregion

    #region unity回调
    private void Start()
    {

        playerModel = GetModel<PlayerModel>();//获取角色模型
        
    }

    private void Update()
    {
        if (LocalPlayer != null)
        {
            CameraFollow();
        }

    }
    #endregion

    #region 事件回调

    public override void NewEvents()
    {
        this.AttentionEvents.Add(Consts.E_CountDownStart);
        this.AttentionEvents.Add(Consts.E_CountDownComplete);
        this.AttentionEvents.Add(Consts.E_End);
    }

    public override void HandleEvent(string eventName, object data)
    {

        switch (eventName)
        {
            case Consts.E_End:

            case Consts.E_CountDownStart:

                StopAllTouch();

                break;

            case Consts.E_CountDownComplete:

                StartAllTouch();

                break;
        }

    }

    #endregion

    #region 帮助方法
    void FindLocalPlayer()
    {

        if (fundLocalPlayer&& fundOtherPlayer)
        {
            return;
        }

            //找出LocalPlayer
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject i in Players)
        {
            if (i.GetComponent<Player>().isLocalPlayer)
            {
                LocalPlayer = i;
                fundLocalPlayer = true;
            }
            else
            {
                OtherPlayer = i;
                fundOtherPlayer = true;
            }
        }
    }

    /// <summary>
    /// 设置技能冷却
    /// </summary>
    private IEnumerator SetSkillCountDown(GameObject skill, int skillNumber,int time)
    {
        for (int i = time * 100; i >= 0; i-- )
        {          
            skill.GetComponent<Image>().fillAmount = i * (0.01f/time);
            yield return new WaitForSeconds(0.01f);
        }
        switch (skillNumber)
        {
            case 1:
                skillWallFinish = true;
                break;
            case 2:
                skillShootFinish = true;
                break;
            case 3:
                skillPitchFireFinish = true;
                break;
        }
    }

    #endregion

}
