using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomToJoinController : MonoBehaviour {

    public string roomName;
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
