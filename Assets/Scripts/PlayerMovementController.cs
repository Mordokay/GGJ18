using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float velocity = 2.0f;

    private PhotonView m_photon_view;
    private Vector3 TargetPosition;
    //Only used if we want to update rotation in the future
    //private Quaternion TargetRotation;
    bool recievedTargetPos;

    private void Start()
    {
        m_photon_view = this.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (m_photon_view.isMine)
            CheckInput();
        else
            SmoothMove();
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
            Debug.Log("Recieved target pos");
            recievedTargetPos = true;
            return;
            //TargetRotation = (Quaternion)stream.ReceiveNext();
        }

        recievedTargetPos = false;
    }

    private void SmoothMove()
    {
        if (recievedTargetPos)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
        }
        else
        {
            transform.position = TargetPosition;
        }
        /*
        if (Vector3.Distance(TargetPosition, transform.position) > 0.2f)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
            Debug.Log(transform.position + " <> " + TargetPosition + " <distance> " + Vector3.Distance(TargetPosition, transform.position));
        }
        */
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 * Time.deltaTime);
    }

    void CheckInput()
    {
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

            if (this.GetComponent<Rigidbody2D>().velocity.magnitude < 0.2f)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }
}