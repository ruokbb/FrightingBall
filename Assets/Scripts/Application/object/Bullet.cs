using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Game.instance.Sound.playEffect("Hurt", 0.7f);
            collision.gameObject.GetComponent<Player>().Attacked(1);
        }
        Destroy(gameObject);
    }

}
