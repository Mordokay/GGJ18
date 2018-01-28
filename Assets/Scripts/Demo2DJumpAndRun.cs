using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Demo2DJumpAndRun : MonoBehaviour 
{
    public void ReturnToLoby()
    {
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.room.EmptyRoomTtl = 1;
        PhotonNetwork.LeaveRoom();
    }

    void OnJoinedRoom()
    {
        if( PhotonNetwork.isMasterClient == false )
        {
            return;
        }
    }

    void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
