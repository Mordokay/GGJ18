using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWaveManager : MonoBehaviour {

    public string State;
    float timeSinceCreation;
    float waveTimeOfLife = 1.5f;

    private void Start()
    {
        timeSinceCreation = 0.0f;
    }

    void Update () {

        if (timeSinceCreation > waveTimeOfLife)
        {
            PhotonView view = GetComponent<PhotonView>();
            if (view.isMine)
                PhotonNetwork.Destroy(this.gameObject);

            return;
        }

        timeSinceCreation += Time.deltaTime;
        
        this.transform.localScale = Vector3.one * timeSinceCreation * 4;

        if (1 - timeSinceCreation / waveTimeOfLife < 0.3f)
        {
            return;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r,
                this.GetComponent<SpriteRenderer>().color.g,
                this.GetComponent<SpriteRenderer>().color.b, 1 - timeSinceCreation / waveTimeOfLife);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PhotonView>().isMine && coll.gameObject.tag == "Player")
        {
            switch (this.gameObject.tag)
            {
                case "WaveA":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("A");
                    break;
                case "WaveC":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("C");
                    break;
                case "WaveG":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("G");
                    break;
                case "WaveT":
                    coll.gameObject.GetComponent<PlayerInfo>().ReceiveState("T");
                    break;
            }
            //coll.gameObject.GetComponent<PlayerInfo>().ReceiveState(State);
            //coll.gameObject.GetComponent<PlayerManager>()._stack_label.text += State;
            //Debug.Log("Player " + coll.gameObject.name + "Hit.");
        }
    }
}
