using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class JumpCommand : Controller
{
    public override void Execute(object data)
    {
        JumpArgs a = data as JumpArgs;
        PlayerModel playerModel = GetModel<PlayerModel>();
        //
        if (a.jumpActivity)
        {
            //起跳
            playerModel.isGround = false;
        }
        else
        {
            //落地
            playerModel.isGround = true;
        }

    }
}
