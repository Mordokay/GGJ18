﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float velocity = 2.0f;

	void Update () {
        bool isClicking = false;

        Vector3 myVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            myVelocity += Vector3.up * velocity;
            isClicking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            myVelocity -= Vector3.right * velocity;
            isClicking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            myVelocity -= Vector3.up * velocity;
            isClicking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myVelocity += Vector3.right * velocity;
            isClicking = true;
        }

        if (isClicking)
        {
            this.GetComponent<Rigidbody2D>().velocity = myVelocity;
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity /= 1.05f;
        }
    }
}
