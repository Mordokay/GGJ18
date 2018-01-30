using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class PlayerManager : MonoBehaviour
    {
       // public List<Sprite> 

        private PlayerInfo _player_info;

        public Text _sequence_label;
        public Text _stack_label;
        public Text _state_label;
        public Text _Nickname;

        PhotonView m_photon_view;
        SpriteRenderer m_sprite;

        public bool game_started = false;

        bool hidding = false;
        float hiddenTime = 0.0f;
        /*
        bool scallingDown = false;
        bool scallingUp = false;
        */
        float timeSinceLastAbilityUse = 0.0f;
        public float abilityCooldown = 1.0f;
        public bool gameEnded;

        void Start()
        {
            m_photon_view = GetComponent<PhotonView>();
            m_sprite = GetComponentInParent<SpriteRenderer>();

            _player_info = GetComponent<PlayerInfo>();
            _player_info.SetUp();
            
           _Nickname.text = _player_info._player_name;
            
            gameEnded = false;
            
            //foreach (PhotonPlayer player in PhotonNetwork.playerList)
            //{
            //    Debug.Log("Looking: " + player.NickName);
            //    Debug.Log("ViewName: " + m_photon_view.name);
            //    if (player.NickName)
            //    {
            //        Debug.Log("Got: " + player.NickName);
            //        _Nickname.text = PhotonNetwork.playerName;
            //    }
            //}
            

            _stack_label.text = "";
            _sequence_label.text = "";
            _state_label.text = "";
            
            Vector3 sequence_label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            Vector3 stack_label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            _sequence_label.gameObject.transform.position = sequence_label_position;
            _stack_label.gameObject.transform.position = stack_label_position;

            

            if (PhotonNetwork.playerList.Count() <= PlayerConsts.PLAYER_NUMBER)
            {
                GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(2).GetComponentInChildren<Text>().text = "Waiting for 2 Players...";
            }
        }

        void CheckWin()
        {
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(player.GetComponent<PlayerInfo>().Sequence != "" && player.GetComponent<PlayerInfo>().Sequence.Equals(player.GetComponent<PlayerInfo>().Stack))
                {
                    if (player.GetComponent<PhotonView>().isMine)
                    {
                        GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(0).gameObject.SetActive(true);
                        GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = "You Win!";
                        Debug.Log("You Win!!!");
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(1).gameObject.SetActive(true);
                        GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = "You Lost! "+ System.Environment.NewLine + " Player " + player.GetComponent<PlayerInfo>()._player_name + " won.";
                        Debug.Log("You Lose!!!");
                    }
                    gameEnded = true;
                    //Time.timeScale = 0.0f;
                }
            }
        }

        void Update()
        {
            CheckWin();
            if (PhotonNetwork.playerList.Count() >= PlayerConsts.PLAYER_NUMBER)
            {
                game_started = true;
                GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(2).gameObject.SetActive(false);
            }

            if (game_started)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    int hasA = 0;
                    int hasG = 0;
                    int hasC = 0;
                    int hasT = 0;

                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        switch (player.GetComponent<PlayerInfo>().CurrentState)
                        {
                            case "A":
                                hasA += 1;
                                break;
                            case "C":
                                hasC += 1;
                                break;
                            case "G":
                                hasG += 1;
                                break;
                            case "T":
                                hasT += 1;
                                break;
                        }
                    }

                    if (GameObject.FindGameObjectWithTag("ResourceA") != null)
                    {
                        hasA += GameObject.FindGameObjectsWithTag("ResourceA").Length;
                    }
                    if (GameObject.FindGameObjectWithTag("ResourceT") != null)
                    {
                        hasT += GameObject.FindGameObjectsWithTag("ResourceT").Length;
                    }
                    if (GameObject.FindGameObjectWithTag("ResourceG") != null)
                    {
                        hasG += GameObject.FindGameObjectsWithTag("ResourceG").Length;
                    }
                    if (GameObject.FindGameObjectWithTag("ResourceC") != null)
                    {
                        hasC += GameObject.FindGameObjectsWithTag("ResourceC").Length;
                    }

                    while (hasA <= 1)
                    {
                        PhotonNetwork.Instantiate("ResourceA", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                            UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
                        hasA += 1;
                    }
                    while (hasT <= 1)
                    {
                        PhotonNetwork.Instantiate("ResourceT", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                             UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
                        hasT += 1;
                    }
                    while (hasG <= 1)
                    {
                        PhotonNetwork.Instantiate("ResourceG", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                            UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
                        hasG += 1;
                    }
                    while (hasC <= 1)
                    {
                        PhotonNetwork.Instantiate("ResourceC", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                            UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
                        hasC += 1;
                    }
                    //Debug.Log("I am the master!!! with " + result);
                }
                else
                {
                    //Debug.Log("I am NOT the master!!!");
                }
                if (hidding)
                {
                    hiddenTime += Time.deltaTime;
                    if (hiddenTime > 0.5f)
                    {
                        hidding = false;
                        hiddenTime = 0.0f;
                        this.GetComponent<CircleCollider2D>().enabled = true;

                        StartCoroutine(FadeTo(1.0f, 0.5f));
                    }
                }
                else
                {
                    timeSinceLastAbilityUse += Time.deltaTime;
                }

                if (m_photon_view.isMine)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (!hidding && timeSinceLastAbilityUse > abilityCooldown)
                        {
                            switch (_player_info.CurrentState)
                            {
                                case "A":
                                    PhotonNetwork.Instantiate("WaveA", this.transform.position, Quaternion.identity, 0);
                                    break;
                                case "C":
                                    PhotonNetwork.Instantiate("WaveC", this.transform.position, Quaternion.identity, 0);
                                    break;
                                case "G":
                                    PhotonNetwork.Instantiate("WaveG", this.transform.position, Quaternion.identity, 0);
                                    break;
                                case "T":
                                    PhotonNetwork.Instantiate("WaveT", this.transform.position, Quaternion.identity, 0);
                                    break;
                            }
                            //GameObject wave = PhotonNetwork.Instantiate("Wave", this.transform.position, Quaternion.identity, 0);
                            //wave.GetComponent<SingleWaveManager>().State = _player_info.CurrentState;

                            hidding = true;
                            //scallingDown = true;
                            timeSinceLastAbilityUse = 0.0f;

                            this.GetComponent<CircleCollider2D>().enabled = false;

                            PhotonNetwork.Instantiate("WaveSound1", this.transform.position, Quaternion.identity, 0);

                            StartCoroutine(FadeTo(0.1f, 0.5f));
                        }
                    }
                }
            }
            //_sequence_label.text = _player_info.Sequence;
            //_stack_label.text = _player_info.Stack;
            //_state_label.text = _player_info.CurrentState;

        }

        IEnumerator FadeTo(float aValue, float aTime)
        {
            float alpha = m_sprite.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
                m_sprite.color = newColor;
                yield return null;
            }
        }
    }
}
