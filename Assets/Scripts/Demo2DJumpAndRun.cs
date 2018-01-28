using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Demo2DJumpAndRun : MonoBehaviour 
{
    public void ReturnToLoby()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.room.EmptyRoomTtl = 5;
        SceneManager.LoadScene(0);
    }

    void OnJoinedRoom()
    {
        if( PhotonNetwork.isMasterClient == false )
        {
            return;
        }
        
       // PhotonNetwork.InstantiateSceneObject( "Physics Box", new Vector3( -4.5f, 5.5f, 0 ), Quaternion.identity, 0, null );
       // PhotonNetwork.InstantiateSceneObject( "Physics Box", new Vector3( -4.5f, 4.5f, 0 ), Quaternion.identity, 0, null );
       // PhotonNetwork.InstantiateSceneObject( "Physics Box", new Vector3( -4.5f, 3.5f, 0 ), Quaternion.identity, 0, null );

        // PhotonNetwork.InstantiateSceneObject( "Physics Box", new Vector3( 4.5f, 5.5f, 0 ), Quaternion.identity, 0, null );
        // PhotonNetwork.InstantiateSceneObject( "Physics Box", new Vector3( 4.5f, 4.5f, 0 ), Quaternion.identity, 0, null );
        // PhotonNetwork.InstantiateSceneObject( "Physics Box", new Vector3( 4.5f, 3.5f, 0 ), Quaternion.identity, 0, null );
    }
}
