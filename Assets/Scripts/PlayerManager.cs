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
        private PlayerInfo info;

        public Text _sequence_label;
        public Text _state_label;
        PhotonView m_photon_view;

        int counter;

        bool hidding = false;
        bool scallingDown = false;
        bool scallingUp = false;
        float hiddenTime = 0.0f;

        float timeSinceLastAbilityUse = 0.0f;
        public float abilityCooldown = 1.0f;

        void Start()
        {
            //just for testing purposes
            counter = 0;

            info = GetComponent<PlayerInfo>();
            info.SetUp();
            Vector3 label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            _sequence_label.gameObject.transform.position = label_position;

            m_photon_view = GetComponent<PhotonView>();
        }

        void Update()
        {
            if (hidding)
            {
                hiddenTime += Time.deltaTime;
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
            else
            {
                timeSinceLastAbilityUse += Time.deltaTime;
            }

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
                        Instantiate(Resources.Load("WaveController"), this.transform.transform);

                        hidding = true;
                        scallingDown = true;
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
            //_sequence_label.text = new string(info.GetSequence().ToArray<char>());
            //_state_label.text = info.GetState().ToString();
        }

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(_sequence_label.text);
                stream.SendNext(counter);
                //stream.SendNext(transform.rotation);
            }
            else
            {
                _sequence_label.text = (string)stream.ReceiveNext();
                counter = (int)stream.ReceiveNext();
                _state_label.text = counter.ToString();
                //TargetRotation = (Quaternion)stream.ReceiveNext();
            }
        }

    }
}
