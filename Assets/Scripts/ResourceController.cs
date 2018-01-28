using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            switch (this.gameObject.tag)
            {
                case "ResourceA":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("A");
                    PhotonNetwork.Destroy(this.gameObject);
                    break;
                case "ResourceC":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("C");
                    PhotonNetwork.Destroy(this.gameObject);
                    break;
                case "ResourceG":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("G");
                    PhotonNetwork.Destroy(this.gameObject);
                    break;
                case "ResourceT":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("T");
                    PhotonNetwork.Destroy(this.gameObject);
                    break;
            }
        }
    }
}
