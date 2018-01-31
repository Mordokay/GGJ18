using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float forceSpeed;

    private void Start()
    {
        Vector3 myPos = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject myTitlescreen = Instantiate(Resources.Load("Titlescreen"), this.transform) as GameObject;
        myTitlescreen.transform.position = myPos;
        myTitlescreen.GetComponent<Rigidbody2D>().AddForce(Vector2.left * forceSpeed);

        myPos = new Vector3(23.0f, 0.0f, 0.0f);
        myTitlescreen = Instantiate(Resources.Load("Titlescreen"), this.transform) as GameObject;
        myTitlescreen.transform.position = myPos;
        myTitlescreen.GetComponent<Rigidbody2D>().AddForce(Vector2.left * forceSpeed);
    }

    void Update()
    {
        
        if(this.transform.GetChild(this.transform.childCount - 1).position.x < 0.0f)
        {
            Vector3 myPos = new Vector3(this.transform.GetChild(this.transform.childCount - 1).position.x + 23.0f, 0.0f, 0.0f);
            GameObject myTitlescreen = Instantiate(Resources.Load("Titlescreen"), this.transform) as GameObject;
            myTitlescreen.transform.position = myPos;
            myTitlescreen.GetComponent<Rigidbody2D>().AddForce(Vector2.left * forceSpeed);
        }

        if(this.transform.GetChild(0).position.x < -23.0f)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
        
    }
}
