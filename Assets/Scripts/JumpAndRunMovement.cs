using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JumpAndRunMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    Animator m_Animator;
    Rigidbody2D m_Body;
    PhotonView m_PhotonView;
    public Text myLabel;

    bool m_IsGrounded;

    PhotonPlayer[] players;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Body = GetComponent<Rigidbody2D>();
        m_PhotonView = GetComponent<PhotonView>();

        if (m_PhotonView.isOwnerActive)
        {
            myLabel.text = m_PhotonView.ownerId.ToString();
            players = PhotonNetwork.playerList;
        }
    }

    void Update()
    {
        UpdateIsGrounded();
        UpdateIsRunning();
        UpdateFacingDirection();

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            m_PhotonView.RPC("UpdateLabel", PhotonTargets.All, "hello", 0, PhotonNetwork.playerName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_PhotonView.RPC("UpdateLabel", PhotonTargets.All, "hello", 1, PhotonNetwork.playerName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_PhotonView.RPC("UpdateLabel", PhotonTargets.All, "hello", 2, PhotonNetwork.playerName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_PhotonView.RPC("UpdateLabel", PhotonTargets.All, "hello", 3, PhotonNetwork.playerName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_PhotonView.RPC("UpdateLabel", PhotonTargets.All, "hello", 4, PhotonNetwork.playerName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_PhotonView.RPC("UpdateLabel", PhotonTargets.All, "hello", 5, PhotonNetwork.playerName);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PhotonNetwork.player.ID != m_PhotonView.ownerId)
            {
                m_PhotonView.RPC("UpdateLabel2", PhotonTargets.All, Random.Range(0, 100), m_PhotonView.ownerId);
            }
        }
    }

    void FixedUpdate()
    {
        if( m_PhotonView.isMine == false )
        {
            return;
        }

        UpdateMovement();
        UpdateJumping();
    }

    void UpdateFacingDirection()
    {
        if( m_Body.velocity.x > 0.2f )
        {
            transform.localScale = new Vector3( 1, 1, 1 );
        }
        else if( m_Body.velocity.x < -0.2f )
        {
            transform.localScale = new Vector3( -1, 1, 1 );
        }
    }

    void UpdateJumping()
    {
        if (Input.GetButton("Jump") && m_IsGrounded)
        {
            m_Animator.SetTrigger("IsJumping");
            m_Body.AddForce(Vector2.up * JumpForce);
            m_PhotonView.RPC("DoJump", PhotonTargets.Others);
        }
    }

    [PunRPC]
    void DoJump()
    {
        m_Animator.SetTrigger( "IsJumping" );
    }

    [PunRPC]
    void UpdateLabel2(int message, int ownerID)
    {
        if (m_PhotonView.ownerId == ownerID)
        {
            Debug.Log("ownerID " + ownerID);
            myLabel.text = message.ToString();
        }
    }

    [PunRPC]
    void UpdateLabel(string message, int value, string playerStringID)
    {
        if (PhotonNetwork.playerName.Equals(playerStringID))
        {
            myLabel.text = message + " <V> " + value + "Player";
        }
        //myLabel.text = message + " <V> " + value;
    }

    void UpdateMovement()
    {
        Vector2 movementVelocity = m_Body.velocity;

        if( Input.GetAxisRaw( "Horizontal" ) > 0.5f )
        {
            movementVelocity.x = Speed;

        }
        else if( Input.GetAxisRaw( "Horizontal" ) < -0.5f )
        {
            movementVelocity.x = -Speed;
        }
        else
        {
            movementVelocity.x = 0;
        }

        m_Body.velocity = movementVelocity;
    }

    void UpdateIsRunning()
    {
        m_Animator.SetBool( "IsRunning", Mathf.Abs( m_Body.velocity.x ) > 0.1f );
    }

    void UpdateIsGrounded()
    {
        Vector2 position = new Vector2( transform.position.x, transform.position.y );

        //RaycastHit2D hit = Physics2D.Raycast( position, -Vector2.up, 0.1f, 1 << LayerMask.NameToLayer( "Ground" ) );
        RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.up, 0.1f);

        m_IsGrounded = hit.collider != null;
        m_Animator.SetBool( "IsGrounded", m_IsGrounded );
    }
}
