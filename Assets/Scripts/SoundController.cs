using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	void Update () {
        if(this.GetComponent<PhotonView>().isMine && !this.GetComponent<AudioSource>().isPlaying)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
	}
}
