using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {


    public GameObject player;

    void Update () {
        if(player != null)
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
        }
    }
}