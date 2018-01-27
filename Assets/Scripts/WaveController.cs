using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    bool bigOne = false;
    int  waveCounter = 1;
    float waveInterval = 0.0f;

    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
        GameObject myWave = Instantiate(Resources.Load("Wave")) as GameObject;
        myWave.transform.position = this.transform.position;
    }

    void Update () {
        /*
       waveInterval += Time.deltaTime;

       if(waveInterval > 0.2f && waveCounter < 4)
       {
           if (bigOne)
           {
               GameObject myWave = Instantiate(Resources.Load("Wave"), this.transform.transform) as GameObject;
               Destroy(myWave, 2.0f);
               bigOne = false;
           }
           else
           {
               GameObject myWave = Instantiate(Resources.Load("Wave2"), this.transform.transform) as GameObject;
               Destroy(myWave, 2.0f);
               bigOne = true;
           }
           waveInterval = 0.0f;
           waveCounter += 1;
       }
       */

    }
}