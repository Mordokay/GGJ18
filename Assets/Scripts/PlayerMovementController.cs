using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float velocity = 3f;

    private PhotonView m_photon_view;
    private Vector3 TargetPosition;
    //Only used if we want to update rotation in the future
    //private Quaternion TargetRotation;

    private void Start()
    {
        m_photon_view = this.GetComponent<PhotonView>();
    }

    void Update()
    {
        if(GetComponent<PlayerManager>().game_started && !GetComponent<PlayerManager>().gameEnded)
        if(GetComponent<PlayerManager>().game_started)
        {
            if (m_photon_view.isMine)
                CheckInput();
            else
                SmoothMove();
        }
        
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            //stream.SendNext(transform.rotation);
        }
        else
        {
            TargetPosition = (Vector3)stream.ReceiveNext();
            //TargetRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void SmoothMove()
    {
        //if(Vector3.Distance(TargetPosition, transform.position) > 0.05f)
        //{
            transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.3f);
        //}
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 * Time.deltaTime);
    }

    void CheckInput()
    {
        bool isClicking = false;

        Vector3 myVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            myVelocity += Vector3.up;
            isClicking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            myVelocity -= Vector3.right;
            isClicking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            myVelocity -= Vector3.up;
            isClicking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myVelocity += Vector3.right;
            isClicking = true;
        }

        if (isClicking)
            this.GetComponent<Rigidbody2D>().velocity = myVelocity.normalized * velocity;
        else
            this.GetComponent<Rigidbody2D>().velocity /= 1.05f;

        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, -17.0f, 17.0f),
            Mathf.Clamp(this.transform.position.y, -13.0f, 13.4f), this.transform.position.z);
    }
}