using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Game.instance.Sound.playEffect("Hurt", 0.7f);
            other.gameObject.GetComponent<Player>().Attacked(10);
            Destroy(gameObject);
        }

        if(other.tag == "Wall")
        {
            Destroy(other.gameObject);
        }

    }

}
