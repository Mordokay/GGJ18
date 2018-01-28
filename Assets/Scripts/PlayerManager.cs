using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class PlayerManager : MonoBehaviour
    {
        private PlayerInfo _player_info;

        public Text _sequence_label;
        public Text _stack_label;
        public Text _state_label;
        PhotonView m_photon_view;

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
            if (hidding)
            {
                hiddenTime += Time.deltaTime;
                if (hiddenTime > 2.0f)
                {
                    hidding = false;
                    hiddenTime = 0.0f;
                    this.GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else
            {
                timeSinceLastAbilityUse += Time.deltaTime;
            }
            /*
            if (scallingDown)
            {
                if(hiddenTime < 0.25f)
                {
                    this.transform.localScale = Vector3.one * ((0.25f - hiddenTime) / 0.25f);
                }
                else
                {
                    scallingDown = false;
                }
            }
            else if (scallingUp)
            {
                if (hiddenTime < 2.0f)
                {
                    this.transform.localScale = Vector3.one * ((hiddenTime - 1.75f) / 0.25f);
                }
                else
                {
                    scallingUp = false;
                    hidding = false;
                    this.transform.localScale = Vector3.one;
                    hiddenTime = 0.0f;
                    this.GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else
            {
                this.GetComponent<CircleCollider2D>().enabled = false;
                if (hiddenTime > 1.75f)
                {
                    scallingUp = true;
                }
                //He is hidden
            }

        }
        */

            if (m_photon_view.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!hidding && timeSinceLastAbilityUse > abilityCooldown)
                    {
                        GameObject wave = PhotonNetwork.Instantiate("Wave", this.transform.position, Quaternion.identity, 0, null);
                        wave.GetComponent<SingleWaveManager>().State = _player_info.CurrentState;

                        hidding = true;
                        //scallingDown = true;
                        timeSinceLastAbilityUse = 0.0f;
                        PhotonNetwork.Instantiate("WaveSound" , this.transform.position, Quaternion.identity, 0, null);
                    }
                }
            }
            _sequence_label.text = new string(_player_info.Sequence.ToArray<char>());
            _stack_label.text = new string(_player_info.Stack.ToArray<char>());
            _state_label.text = _player_info.CurrentState.ToString();
        }

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(new string(_player_info.Sequence.ToArray<char>()));
                stream.SendNext(new string(_player_info.Stack.ToArray<char>()));
                stream.SendNext(_player_info.CurrentState.ToString());

            }
            else
            {
                _player_info.Sequence = new List<char>(stream.ReceiveNext().ToString());
                _player_info.Stack = new List<char>(stream.ReceiveNext().ToString());
                _player_info.CurrentState = ((string)stream.ReceiveNext())[0];
                //Debug.Log(((string)stream.ReceiveNext())[0]);
            }
        }

    }
}
