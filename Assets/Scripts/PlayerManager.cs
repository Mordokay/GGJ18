using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
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
        PhotonView m_photon_view;
        SpriteRenderer m_sprite;

        bool hidding = false;
        float hiddenTime = 0.0f;
        /*
        bool scallingDown = false;
        bool scallingUp = false;
        */
        float timeSinceLastAbilityUse = 0.0f;
        public float abilityCooldown = 1.0f;

        void Start()
        {
            m_sprite = GetComponentInParent<SpriteRenderer>();

            //just for testing purposes
            _stack_label.text = "";
            _sequence_label.text = "";

            _player_info = GetComponent<PlayerInfo>();
            _player_info.SetUp();
            Vector3 sequence_label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            Vector3 stack_label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            _sequence_label.gameObject.transform.position = sequence_label_position;
            _stack_label.gameObject.transform.position = stack_label_position;

            m_photon_view = GetComponent<PhotonView>();
        }

        void Update()
        {
            if (PhotonNetwork.isMasterClient)
            {
                bool hasA = false;
                bool hasG = false;
                bool hasC = false;
                bool hasT = false;

                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    switch (player.GetComponent<PlayerInfo>().CurrentState)
                    {
                        case "A":
                            hasA = true;
                            break;
                        case "C":
                            hasC = true;
                            break;
                        case "G":
                            hasG = true;
                            break;
                        case "T":
                            hasT = true;
                            break;
                    }
                }

                if (GameObject.FindGameObjectWithTag("ResourceA") != null)
                {
                    hasA = true;
                }
                if (GameObject.FindGameObjectWithTag("ResourceT") != null)
                {
                    hasT = true;
                }
                if (GameObject.FindGameObjectWithTag("ResourceG") != null)
                {
                    hasG = true;
                }
                if (GameObject.FindGameObjectWithTag("ResourceC") != null)
                {
                    hasC = true;
                }

                string result = "";
                if (!hasA)
                {
                    PhotonNetwork.Instantiate("ResourceA", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                        UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
                }
                if (!hasT)
                {
                    PhotonNetwork.Instantiate("ResourceT", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                         UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
                }
                if (!hasG)
                {
                    PhotonNetwork.Instantiate("ResourceG", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                        UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
                }
                if (!hasC)
                {
                    PhotonNetwork.Instantiate("ResourceC", new Vector3(UnityEngine.Random.Range(-16.0f, 16.0f),
                        UnityEngine.Random.Range(-13.0f, 13.4f), 0.0f), Quaternion.identity, 0);
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

                        PhotonNetwork.Instantiate("WaveSound" , this.transform.position, Quaternion.identity, 0);

                        StartCoroutine(FadeTo(0.25f, 0.5f));
                    }
                }
            }
            _sequence_label.text = _player_info.Sequence;
            _stack_label.text = _player_info.Stack;
            _state_label.text = _player_info.CurrentState;
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
