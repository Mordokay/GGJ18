using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using ExitGames.Client.Photon;
using UnityEngine.EventSystems;

public class PhotonLobby : MonoBehaviour
{
    private bool bConnectFailed = false;

    public static readonly string SceneNameMenu = "GGJ18Lobby";
    public static readonly string SceneNameGame = "GGJ18";

    public GameObject RoomListHolder;
    public InputField playerNameInputField;
    public InputField roomNameInputField;
    public Text feedbackText;
    int currentRoomListCount;


    public void Awake()
    {
        currentRoomListCount = 0;

        Screen.fullScreen = false;

        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.automaticallySyncScene = true;

        // the following line checks if this client was just created (and not yet online). if so, we connect
        if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated)
        {
            // Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
            PhotonNetwork.ConnectUsingSettings("0.9");
        }

        // if you wanted more debug out, turn this on:
        // PhotonNetwork.logLevel = NetworkLogLevel.Full;
    }

    public void SetPlayerName(InputField input)
    {
        if (input.text.Length > 0)
        {
            PhotonNetwork.playerName = input.text;
            PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
        }
    }


    public void CreateGameRoom()
    {
        if(roomNameInputField.text != "")
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text, new RoomOptions() { MaxPlayers = 10 }, null);
        }
        else
        {
            PhotonNetwork.CreateRoom(Random.Range(100000000, 1000000000).ToString(), new RoomOptions() { MaxPlayers = 10 }, null);
        }
    }

    public void Update()
    {
        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connecting)
            {
                feedbackText.text = "Connecting to: " + PhotonNetwork.ServerAddress;
            }
            else
            {
                feedbackText.text = "Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress;
            }

            if (this.bConnectFailed)
            {
                feedbackText.text = "Connection failed. Check setup and use Setup Wizard to fix configuration.";
                /*
                if (GUILayout.Button("Try Again", GUILayout.Width(100)))
                {
                    this.bConnectFailed = false;
                    PhotonNetwork.ConnectUsingSettings("0.9");
                }
                */
            }
        }
        else
        {
            if (PhotonNetwork.GetRoomList().Length == 0)
            {
                feedbackText.text = "Currently no games are available." +
                    System.Environment.NewLine + "Rooms will be listed here, when they become available.";

                currentRoomListCount = 0;

                foreach (Transform child in RoomListHolder.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            //We check to see if the number of rooms changed. If true Updates the list of rooms
            else if(PhotonNetwork.GetRoomList().Length != currentRoomListCount)
            {
                currentRoomListCount = PhotonNetwork.GetRoomList().Length;

                feedbackText.text = "";

                foreach (Transform child in RoomListHolder.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
                {
                    GameObject myRoom = Instantiate(Resources.Load("RoomToJoin"), RoomListHolder.transform) as GameObject;
                    myRoom.GetComponentInChildren<RoomToJoinController>().roomName = roomInfo.Name;
                    myRoom.transform.GetChild(1).gameObject.GetComponent<Text>().text = roomInfo.Name;
                    myRoom.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
                }
            }
        }
    }

    public void UpdatePlayerName()
    {
        PlayerPrefs.SetString("playerName", playerNameInputField.text);
        PhotonNetwork.playerName = playerNameInputField.text;
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void OnPhotonCreateRoomFailed()
    {
        Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
    }

    public void OnPhotonJoinRoomFailed(object[] cause)
    {
        Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
    }

    public void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.LoadLevel(SceneNameGame);
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected from Photon.");
    }

    public void OnFailedToConnectToPhoton(object parameters)
    {
        this.bConnectFailed = true;
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("As OnConnectedToMaster() got called, the PhotonServerSetting.AutoJoinLobby must be off. Joining lobby by calling PhotonNetwork.JoinLobby().");
        PhotonNetwork.JoinLobby();
    }
}
