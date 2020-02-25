using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPitchFireArea : MonoBehaviour {

    public bool isServerCreat = true;
    Coroutine e;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        if(isServerCreat )
        {
            if (!other.GetComponent<Player>().isLocalPlayer)
            {
                //打开携程，每秒钟收到伤害
                e = StartCoroutine(hurt(other.gameObject));
            }
        }
        else
        {
            if (other.GetComponent<Player>().isLocalPlayer)
            {
                //打开携程，每秒钟收到伤害
                e = StartCoroutine(hurt(other.gameObject));
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        if (isServerCreat)
        {
            if (!other.GetComponent<Player>().isLocalPlayer)
            {
                //停止携程，每秒钟收到伤害
                StopCoroutine(e);
            }
        }
        else
        {
            if (other.GetComponent<Player>().isLocalPlayer)
            {
                //停止携程，每秒钟收到伤害
                StopCoroutine(e);
            }
        }
    }

    /// <summary>
    /// 判断是否是敌人燃烧弹，每秒钟收到伤害
    /// </summary>
    private IEnumerator hurt(GameObject player)
    {
        while (true)
        {
            player.GetComponent<Player>().Attacked(1);
            yield return new WaitForSeconds(0.2f);
        }
        
    }


}
