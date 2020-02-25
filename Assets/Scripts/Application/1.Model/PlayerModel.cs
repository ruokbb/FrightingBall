using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : Model
{

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public float moveSpeed = 5f;
    public bool isGround = true;
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.M_PlayerModel;
        }
    }
    #endregion

    #region 方法
    #endregion

    #region unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion


}
