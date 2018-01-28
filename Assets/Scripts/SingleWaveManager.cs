using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWaveManager : MonoBehaviour {

    public char State { get; set; }
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
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerInfo>().ReceiveState(State);
            Debug.Log("Player " + coll.gameObject.name + "Hit.");
        }
            

    }
}
