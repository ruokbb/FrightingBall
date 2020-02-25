using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {


    //本类绑定在Player身上，借用UIInfo使用model
    //血量存在本类

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    UIInfo ui;

    [SyncVar(hook = "OnChangeHp")]
    public float currentHp = 100;

    public GameObject[] BulletSpawn;
    public GameObject WallSpawn;

    GameObject Bullet;
    GameObject Wall;
    GameObject SkillBullet;
    #endregion

    #region 属性
    #endregion

    #region 方法

    //受到伤害
    public void Attacked(int hp)
    {
        
        if (!isServer) return;

        currentHp -= hp;
        if (currentHp <=0)
        {
            currentHp = 0;
        }
       
    }


    void OnChangeHp(float Hp)
    {

        if (isLocalPlayer)
        {
            if (isServer)
            {
                ui.ServeHp = Hp;
                if (Hp == 0)
                {
                    //死亡逻辑，借用UIInfo,发事件
                    ui.Dead(0);
                }
            }
            else
            {
                ui.ClintHp = Hp;
                if (Hp == 0)
                {
                    //死亡逻辑，借用UIInfo,发事件
                    ui.Dead(1);
                }
            }
        }
        else
        {
            if (isServer)
            {
                ui.ClintHp = Hp;
                if (Hp == 0)
                {
                    //死亡逻辑，借用UIInfo,发事件
                    ui.Dead(1);
                }
            }
            else
            {
                ui.ServeHp = Hp;
                if (Hp == 0)
                {
                    //死亡逻辑，借用UIInfo,发事件
                    ui.Dead(0);
                }
            }
                
        }
    }

    //判断自己是何种攻击方式
    public int WhatSkill()
    {
        return 0;
    }

    /// <summary>
    /// 开始射击
    /// </summary>
    [Command]
    public void CmdFire()
    {

        GameObject BulletR = Instantiate(Bullet,
            BulletSpawn[0].
            transform.position, Bullet.transform.rotation) as GameObject;
        BulletR.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        Destroy(BulletR, 2);
        NetworkServer.Spawn(BulletR);

        GameObject BulletL = Instantiate(Bullet,
            BulletSpawn[1].
            transform.position, Bullet.transform.rotation) as GameObject;
        BulletL.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        Destroy(BulletL, 2);
        NetworkServer.Spawn(BulletL);
    }

    /// <summary>
    /// 技能：建墙
    /// </summary>
    [Command]
    public void CmdSetWall()
    {
        GameObject wall = Instantiate(Wall,
                   WallSpawn.transform.position, 
                   WallSpawn.transform.rotation) as GameObject;
        Destroy(wall, 20);
        NetworkServer.Spawn(wall);
    }

    /// <summary>
    /// 技能：穿甲弹
    /// </summary>
    [Command]
    public void CmdShootBullet()
    {
        GameObject BulletR = Instantiate(SkillBullet,
            BulletSpawn[0].
            transform.position, Bullet.transform.rotation) as GameObject;
        BulletR.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        Destroy(BulletR, 2);
        NetworkServer.Spawn(BulletR);

        GameObject BulletL = Instantiate(SkillBullet,
            BulletSpawn[1].
            transform.position, Bullet.transform.rotation) as GameObject;
        BulletL.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        Destroy(BulletL, 2);
        NetworkServer.Spawn(BulletL);
    }

    /// <summary>
    /// 技能：燃烧弹
    /// </summary>
    [Command]
    public void CmdPitchFire(Vector3 pos)
    {
        //服务器角色释放技能
        if (isLocalPlayer)
        {
            SkillPitch(pos, "AuraFire", "FireShield");
        }
        else
        //客户端角色释放技能
        {
            SkillPitch(pos, "AuraFrost", "FrostShield");

        }             
    }

    /// <summary>
    /// 通知服务器可以进行游戏
    /// </summary>
    [Command]
    public void CmdStartGame()
    {
        ui.StartGame();
    }

    /// <summary>
    /// 开始进行游戏
    /// </summary>
    public void StartGame()
    {
        ui.StartGame();
    }
    #endregion

    #region unity回调

    private void Start()
    {
        ui = GameObject.Find("UIInfo").GetComponent<UIInfo>();
        Bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        Wall = Resources.Load("Prefabs/Wall") as GameObject;
        SkillBullet = Resources.Load("Prefabs/SkillBullet") as GameObject;

        //如果是服务器，显示IP,记录到全局Game
        if (isServer) {
            ui.DisplayIP();
            Game.instance.isServer = true;

            //设置颜色
            if (isLocalPlayer)
                GetComponent<MeshRenderer>().material.color = Color.red;
            else
                GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            Game.instance.isServer = false;

            //设置颜色
            if (isLocalPlayer)
                GetComponent<MeshRenderer>().material.color = Color.blue;
            else
                GetComponent<MeshRenderer>().material.color = Color.red;

        }

        //检测客户端我玩家加入，分别在服务端客户端执行开始游戏操作
        if (!isServer)
        {
            CmdStartGame();
            StartGame();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        ////跟另外一个小球碰撞
        //Player rival = collision.gameObject.GetComponent<Player>();
        //Vector3 pos = collision.contacts[0].point;//碰撞点
        //Vector3 direction = SetDirection(pos);
        //Vector3 directionXY = SetDirectionXY(pos);

        //if (collision.gameObject.tag == "Player")
        //{
        //    //根据对方是何种方式攻击进行判断
        //    switch (rival.WhatSkill())
        //    {
        //        case 0:
        //                break;
        //    }
        //}
    }


    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法

    void SkillPitch(Vector3 pos,string areaName,string fallName )
    {
        //设置地区标识
        GameObject effect = Resources.Load("Prefabs/Spe/" + areaName) as GameObject;
        GameObject a = Instantiate(effect, pos, Quaternion.identity);
        NetworkServer.Spawn(a);

        //设置掉落燃烧弹
        Vector3 posUp = new Vector3(pos.x, pos.y + 10, pos.z);
        GameObject fall = Resources.Load("Prefabs/Spe/" + fallName) as GameObject;
        GameObject b = Instantiate(fall, posUp, Quaternion.identity);
        NetworkServer.Spawn(b);
    }

    #endregion


}
