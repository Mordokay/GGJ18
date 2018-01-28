using Assets.Scripts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlayerInfo : MonoBehaviour
    {
        public string Sequence { get; set; }
        public string Stack { get; set; }

        public string CurrentState;

        public Sprite StateG;
        public Sprite StateT;
        public Sprite StateA;
        public Sprite StateC;

        private System.Random _random_generator;

        public void SetUp()
        {
            Sequence = "";
            Stack = "";
            CurrentState = "";
            _random_generator = new System.Random();

            for (int i = 0; i < PlayerConsts.SEQUENCE_NUMBER; i++) { 
                Sequence += PlayerConsts.SEQUENCE_STATES[UnityEngine.Random.Range(0, PlayerConsts.SEQUENCE_STATES.Length)];
            }

            CurrentState = PlayerConsts.SEQUENCE_STATES[UnityEngine.Random.Range(0, PlayerConsts.SEQUENCE_STATES.Length)];
            ChangeSprite();
        }

        void ChangeSprite()
        {
            switch (CurrentState)
            {
                case "A":
                    this.GetComponent<SpriteRenderer>().color = new Color(165.0f, 0.0f, 0.0f);
                    break;
                case "T":
                    this.GetComponent<SpriteRenderer>().color = new Color(0.0f, 155.0f, 0.0f);
                    break;
                case "G":
                    this.GetComponent<SpriteRenderer>().color = new Color(140.0f, 0.0f, 150.0f);
                    break;
                case "C":
                    this.GetComponent<SpriteRenderer>().color = new Color(0.0f, 145.0f, 250.0f); ;
                    break;
            }
        }

        public void ReceiveState(string state)
        {
            CurrentState = state;
            ChangeSprite();
            Debug.Log("CurrentState: " + CurrentState + " Sequence " + Sequence + " Sequence[Stack.Length] " + Sequence[Stack.Length]);
            if (Sequence[Stack.Length].Equals(state[0]))
            {
                Stack += state;
                if (Sequence.Equals(Stack))
                {
                    Debug.Log("You Win!!!");
                    GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(0).gameObject.SetActive(true);

                    //PhotonNetwork.LeaveRoom();
                    //SceneManager.LoadScene(0);
                }
            }
            else
            {
                Stack = "";
            }
        }

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(Sequence);
                stream.SendNext(Stack);
                stream.SendNext(CurrentState);
                //Debug.Log("Writing!!!");
            }
            else 
            {
                Sequence = (string)stream.ReceiveNext();
                Stack = (string)stream.ReceiveNext();
                CurrentState = (string)stream.ReceiveNext();

                ChangeSprite();
                //Debug.Log(((string)stream.ReceiveNext())[0]);
            }
        }

    }
}
