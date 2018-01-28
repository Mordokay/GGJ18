// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerMenu.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using ExitGames.Client.Photon;
using UnityEngine.EventSystems;

public class PhotonLobby : MonoBehaviour
{
    public GUISkin Skin;
    public Vector2 WidthAndHeight = new Vector2(600, 400);

    private Vector2 scrollPos = Vector2.zero;

    private bool bConnectFailed = false;
    private string _roomName;

    public static readonly string SceneNameMenu = "GGJ18Lobby";

    public static readonly string SceneNameGame = "GGJ18";

    private string errorDialog;
    private double timeToClearDialog;

    public GameObject content;
    public GameObject scroll;
    public GameObject messageBox;
    public InputField roomName;
    public InputField playerName;
    public Button createRoom;

    private List<GameObject> serverList;
    private GameObject selectedObject;

    public string ErrorDialog
    {
        get { return this.errorDialog; }
        private set
        {
            this.errorDialog = value;
            if (!string.IsNullOrEmpty(value))
            {
                this.timeToClearDialog = Time.time + 4.0f;
            }
        }
    }

    public void Awake()
    {
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

    public void OnDisable()
    {
        CancelInvoke();
    }

    /*public void Start()
    {
        if (serverList == null)
        {
            serverList = new List<GameObject>();
        }
        InvokeRepeating("PopulateServerList", 0, 2);

        roomName.onEndEdit.AddListener(delegate { SetRoomName(roomName); });
        playerName.onEndEdit.AddListener(delegate { SetPlayerName(playerName); });
        createRoom.onClick.AddListener(delegate { CreateGameRoom(); });
    }

    public void Update()
    {
        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connecting)
            {
                messageBox.SetActive(true);
                messageBox.transform.Find("Text").GetComponent<Text>().text = "Connecting to: " + PhotonNetwork.ServerAddress;
            }
            else
            {
                messageBox.SetActive(true);
                messageBox.transform.Find("Text").GetComponent<Text>().text = "Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress;
            }

            if (this.bConnectFailed)
            {
                GameObject messageBox = (GameObject)Instantiate(Resources.Load("MessageBoxButton"));
                messageBox.transform.Find("Server").GetComponent<Text>().text = String.Format("Server: {0}", new object[] { PhotonNetwork.ServerAddress });
                messageBox.transform.Find("Server").GetComponent<Text>().text = "AppId: " + PhotonNetwork.PhotonServerSettings.AppID.Substring(0, 8) + "****"; // only show/log first 8 characters. never log the full AppId.
                messageBox.transform.Find("Button").GetComponent<Button>().transform.Find("Text").GetComponent<Text>().text = "TryAgain";
                messageBox.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate { this.bConnectFailed = false; PhotonNetwork.ConnectUsingSettings("0.9"); });
            }

            return;
        }
    }*/

    public void PopulateServerList()
    {
        int i = 0;
        RoomInfo[] hostData = PhotonNetwork.GetRoomList();

        int selected = serverList.IndexOf(selectedObject);

        for (int j = 0; j < serverList.Count; j++)
        {
            Destroy(serverList[j]);
        }
        serverList.Clear();

        if (null != hostData)
        {
            for (i = 0; i < hostData.Length; i++)
            {
                if (!hostData[i].IsOpen)
                    continue;
                
                GameObject text = (GameObject)Instantiate(Resources.Load("RoomInfo"));
                serverList.Add(text);
                text.transform.SetParent(content.transform, false);
                string room = text.transform.Find("RoomName").GetComponent<Text>().text = hostData[i].Name;
                text.transform.Find("PlayerCount").GetComponent<Text>().text = hostData[i].PlayerCount + "/" + hostData[i].MaxPlayers;
                text.transform.Find("PlayersReady").GetComponent<Text>().text = "";
                text.transform.Find("JoinRoom_Button").GetComponent<Button>().onClick.AddListener(delegate { PhotonNetwork.JoinRoom(room); });
                text.transform.Find("StartGame_Button").GetComponent<Button>().onClick.AddListener(delegate { /*PhotonNetwork.StartGame();*/ });
                text.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (i * -25), 0);
            }
        }
        /*if ((i * -25) < -290)
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(400, (i * 25) + 30);
            scroll.SetActive(true);
        }
        else
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 320);
            scroll.SetActive(false);
        }*/
    }

    public void SetPlayerName(InputField input)
    {
        if (input.text.Length > 0)
        {
            PhotonNetwork.playerName = input.text;
            PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
        }
    }

    public void SetRoomName(InputField input)
    {
        if (input.text.Length > 0)
        {
            _roomName = input.text;
        }
    }

    public void CreateGameRoom()
    {
        if(_roomName.Length > 0)
        {
            PhotonNetwork.CreateRoom(this._roomName, new RoomOptions() { MaxPlayers = 5 }, null);
        }
    }

    public void OnGUI()
    {
        if (this.Skin != null)
        {
            GUI.skin = this.Skin;
        }

        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connecting)
            {
                GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress);
            }
            else
            {
                GUILayout.Label("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
            }

            if (this.bConnectFailed)
            {
                GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
                GUILayout.Label(String.Format("Server: {0}", new object[] { PhotonNetwork.ServerAddress }));
                GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID.Substring(0, 8) + "****"); // only show/log first 8 characters. never log the full AppId.

                if (GUILayout.Button("Try Again", GUILayout.Width(100)))
                {
                    this.bConnectFailed = false;
                    PhotonNetwork.ConnectUsingSettings("0.9");
                }
            }

            return;
        }

        Rect content = new Rect((Screen.width - this.WidthAndHeight.x) / 2, (Screen.height - this.WidthAndHeight.y) / 2, this.WidthAndHeight.x, this.WidthAndHeight.y);
        GUI.Box(content, "Join or Create Room");
        GUILayout.BeginArea(content);

        GUILayout.Space(40);

        // Player name
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player name:", GUILayout.Width(150));
        PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
        GUILayout.Space(158);
        if (GUI.changed)
        {
            // Save name
            PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        // Join room by title
        GUILayout.BeginHorizontal();
        GUILayout.Label("Roomname:", GUILayout.Width(150));
        this._roomName = GUILayout.TextField(this._roomName);

        if (GUILayout.Button("Create Room", GUILayout.Width(150)))
        {
            PhotonNetwork.CreateRoom(_roomName, new RoomOptions() { MaxPlayers = 10 }, null);
        }

        GUILayout.EndHorizontal();

        // Create a room (fails if exist!)
       /* GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        //this.roomName = GUILayout.TextField(this.roomName);
        if (GUILayout.Button("Join Room", GUILayout.Width(150)))
        {
            //PhotonNetwork.JoinRoom(this.roomName);
        }

        GUILayout.EndHorizontal();
		*/


        if (!string.IsNullOrEmpty(ErrorDialog))
        {
            GUILayout.Label(ErrorDialog);

            if (this.timeToClearDialog < Time.time)
            {
                this.timeToClearDialog = 0;
                ErrorDialog = "";
            }
        }

        GUILayout.Space(15);

        // Join random room
        /*GUILayout.BeginHorizontal();

        GUILayout.Label(PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " rooms.");
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Join Random", GUILayout.Width(150)))
        {
            PhotonNetwork.JoinRandomRoom();
        }


        GUILayout.EndHorizontal();*/

        GUILayout.Space(15);
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("Currently no games are available.");
            GUILayout.Label("Rooms will be listed here, when they become available.");
        }
        else
        {
            GUILayout.Label(PhotonNetwork.GetRoomList().Length + " rooms available:");

            // Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            this.scrollPos = GUILayout.BeginScrollView(this.scrollPos);
            foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(roomInfo.Name + " " + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers);
                if (GUILayout.Button("Join", GUILayout.Width(150)))
                {
                    PhotonNetwork.JoinRoom(roomInfo.Name);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }

        GUILayout.EndArea();
    }

    //We have two options here: we either joined(by title, list or random) or created a room.

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void OnPhotonCreateRoomFailed()
    {
        ErrorDialog = "Error: Can't create room (room name maybe already used).";
        Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
    }

    public void OnPhotonJoinRoomFailed(object[] cause)
    {
        ErrorDialog = "Error: Can't join room (full or unknown room name). " + cause[1];
        Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
    }

    public void OnPhotonRandomJoinFailed()
    {
        ErrorDialog = "Error: Can't join random room (none found).";
        Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.LoadLevel(SceneNameGame);
        //Time.timeScale = 1.0f;
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
