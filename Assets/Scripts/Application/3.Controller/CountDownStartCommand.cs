using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class CountDownStartCommand : Controller
{
    public override void Execute(object data)
    {
        CountDownStartArgs arg = data as CountDownStartArgs;

        //设置初始位置
        arg.ServerPlayer.transform.position = arg.ServerPos;
        arg.ClientPlayer.transform.position = arg.ClientPos;

        //设置客户端初始相机位置
        if (!arg.ClientPlayer.GetComponent<Player>().isServer)
        {
            GameObject camera = GameObject.FindGameObjectWithTag("Camera");
            camera.transform.position = arg.ClientPlayer.transform.position;
            camera.transform.rotation = Quaternion.Euler(1, -90, 1);
        }
    }
}
