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

        int counter;

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
            counter = 0;
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


            Vector3 label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            _sequence_label.gameObject.transform.position = label_position;

            if (m_photon_view.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!hidding && timeSinceLastAbilityUse > abilityCooldown)
                    {
                        counter += 1;
                        _state_label.text = counter.ToString();
                        PhotonNetwork.Instantiate("Wave", this.transform.position, Quaternion.identity, 0, null);

                        hidding = true;
                        //scallingDown = true;
                        timeSinceLastAbilityUse = 0.0f;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _sequence_label.text = "1111111";
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _sequence_label.text = "222222";
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _sequence_label.text = "333333";
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    _sequence_label.text = "444444";
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
                stream.SendNext(_sequence_label.text);
                stream.SendNext(counter);
                //stream.SendNext(transform.rotation);
                stream.SendNext(new string(_player_info.Sequence.ToArray<char>()));
                stream.SendNext(new string(_player_info.Stack.ToArray<char>()));
                stream.SendNext(_player_info.CurrentState.ToString());
            }
            else
            {
                _sequence_label.text = (string)stream.ReceiveNext();
                counter = (int)stream.ReceiveNext();
                _state_label.text = counter.ToString();

                _player_info.Sequence = new List<char>(stream.ReceiveNext().ToString());
                _player_info.Stack = new List<char>(stream.ReceiveNext().ToString());
                _player_info.CurrentState = stream.ReceiveNext().ToString().ToCharArray()[0];

                //TargetRotation = (Quaternion)stream.ReceiveNext();
            }
        }

    }
}
