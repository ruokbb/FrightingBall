using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FallDown : MonoBehaviour {

    public bool isCreatByServer;

	// Update is called once per frame
	void Update () {
		

        if(transform.position.y < 0.8f)
        {
            if (isCreatByServer)
            {
                //设置触碰体
                GameObject area = Resources.Load("Prefabs/SkillPitchFireArea") as GameObject;
                GameObject b = Instantiate(area, this.transform.position, Quaternion.identity);
                b.GetComponent<SkillPitchFireArea>().isServerCreat = true;

                //设置罩子
                GameObject shiled = Resources.Load("Prefabs/Spe/SkillPitchFire1") as GameObject;
                GameObject c = Instantiate(shiled, this.transform.position, Quaternion.identity);               
            }
            else
            {
                //设置触碰体
                GameObject area = Resources.Load("Prefabs/SkillPitchFireArea") as GameObject;
                GameObject b = Instantiate(area, transform.position, Quaternion.identity);
                b.GetComponent<SkillPitchFireArea>().isServerCreat = false;

                //设置罩子
                GameObject shiled = Resources.Load("Prefabs/Spe/SkillPitchFire2") as GameObject;
                GameObject c = Instantiate(shiled, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);

        }

	}
}
