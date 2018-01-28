using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWaveManager : MonoBehaviour {

    float timeSinceCreation;
    float waveTimeOfLife = 1.5f;

    private void Start()
    {
        timeSinceCreation = 0.0f;
    }

    void Update () {

        if (timeSinceCreation > waveTimeOfLife)
        {
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);

    }
}
