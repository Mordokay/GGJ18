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
        public string Sequence;
        public string Stack;

        public string _player_name;

        public string CurrentState;

        public Sprite StateG;
        public Sprite StateT;
        public Sprite StateA;
        public Sprite StateC;

        private System.Random _random_generator;
        GameObject playerCanvas;

        public void SetUp()
        {
            _player_name = PhotonNetwork.playerName;
            playerCanvas = this.transform.GetChild(0).gameObject;
            Sequence = "";
            Stack = "";
            CurrentState = "";
            _random_generator = new System.Random();

            for (int i = 0; i < PlayerConsts.SEQUENCE_NUMBER; i++)
            {
                Sequence += PlayerConsts.SEQUENCE_STATES[UnityEngine.Random.Range(0, PlayerConsts.SEQUENCE_STATES.Length)];
            }

            CurrentState = PlayerConsts.SEQUENCE_STATES[UnityEngine.Random.Range(0, PlayerConsts.SEQUENCE_STATES.Length)];
            ChangeSprite();

            SetupSequence();
        }

        void CleanSequenceLetters()
        {
            playerCanvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(1).GetChild(0).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(2).GetChild(0).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(2).GetChild(0).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(2).GetChild(0).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(3).GetChild(0).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(3).GetChild(0).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(3).GetChild(0).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(4).GetChild(0).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(4).GetChild(0).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(4).GetChild(0).GetChild(3).gameObject.SetActive(false);
        }

        void CleanStackLetters()
        {
            playerCanvas.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(0).GetChild(1).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(1).GetChild(1).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(2).GetChild(1).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(3).GetChild(1).GetChild(3).gameObject.SetActive(false);

            playerCanvas.transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(false);
            playerCanvas.transform.GetChild(4).GetChild(1).GetChild(3).gameObject.SetActive(false);
        }
        void SetupSequence()
        {
            CleanSequenceLetters();
            //letter , unselected, specific image
            switch (Sequence[0])
            {
                case 'A':
                    playerCanvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    break;
                case 'T':
                    playerCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    break;
                case 'G':
                    playerCanvas.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                    break;
                case 'C':
                    playerCanvas.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
                    break;
            }
            switch (Sequence[1])
            {
                case 'A':
                    playerCanvas.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    break;
                case 'T':
                    playerCanvas.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    break;
                case 'G':
                    playerCanvas.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(true);
                    break;
                case 'C':
                    playerCanvas.transform.GetChild(1).GetChild(0).GetChild(3).gameObject.SetActive(true);
                    break;
            }
            switch (Sequence[2])
            {
                case 'A':
                    playerCanvas.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    break;
                case 'T':
                    playerCanvas.transform.GetChild(2).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    break;
                case 'G':
                    playerCanvas.transform.GetChild(2).GetChild(0).GetChild(2).gameObject.SetActive(true);
                    break;
                case 'C':
                    playerCanvas.transform.GetChild(2).GetChild(0).GetChild(3).gameObject.SetActive(true);
                    break;
            }
            switch (Sequence[3])
            {
                case 'A':
                    playerCanvas.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    break;
                case 'T':
                    playerCanvas.transform.GetChild(3).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    break;
                case 'G':
                    playerCanvas.transform.GetChild(3).GetChild(0).GetChild(2).gameObject.SetActive(true);
                    break;
                case 'C':
                    playerCanvas.transform.GetChild(3).GetChild(0).GetChild(3).gameObject.SetActive(true);
                    break;
            }
            switch (Sequence[4])
            {
                case 'A':
                    playerCanvas.transform.GetChild(4).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    break;
                case 'T':
                    playerCanvas.transform.GetChild(4).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    break;
                case 'G':
                    playerCanvas.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(true);
                    break;
                case 'C':
                    playerCanvas.transform.GetChild(4).GetChild(0).GetChild(3).gameObject.SetActive(true);
                    break;
            }
        }

        void UpdateStack()
        {
            CleanStackLetters();

            if (Stack.Length > 0)
            {
                //Debug.Log("change frst letter: " + Stack);
                //letter , unselected/selected , specific image
                switch (Stack[0])
                {

                    case 'A':
                        playerCanvas.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
                        break;
                    case 'T':
                        playerCanvas.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
                        break;
                    case 'G':
                        playerCanvas.transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(true);
                        break;
                    case 'C':
                        playerCanvas.transform.GetChild(0).GetChild(1).GetChild(3).gameObject.SetActive(true);
                        break;
                }
            }
            if (Stack.Length > 1)
            {
                switch (Stack[1])
                {
                    case 'A':
                        playerCanvas.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
                        break;
                    case 'T':
                        playerCanvas.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(true);
                        break;
                    case 'G':
                        playerCanvas.transform.GetChild(1).GetChild(1).GetChild(2).gameObject.SetActive(true);
                        break;
                    case 'C':
                        playerCanvas.transform.GetChild(1).GetChild(1).GetChild(3).gameObject.SetActive(true);
                        break;
                }
            }
            if (Stack.Length > 2)
            {
                switch (Stack[2])
                {
                    case 'A':
                        playerCanvas.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
                        break;
                    case 'T':
                        playerCanvas.transform.GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(true);
                        break;
                    case 'G':
                        playerCanvas.transform.GetChild(2).GetChild(1).GetChild(2).gameObject.SetActive(true);
                        break;
                    case 'C':
                        playerCanvas.transform.GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(true);
                        break;
                }
            }
            if (Stack.Length > 3)
            {
                switch (Stack[3])
                {
                    case 'A':
                        playerCanvas.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
                        break;
                    case 'T':
                        playerCanvas.transform.GetChild(3).GetChild(1).GetChild(1).gameObject.SetActive(true);
                        break;
                    case 'G':
                        playerCanvas.transform.GetChild(3).GetChild(1).GetChild(2).gameObject.SetActive(true);
                        break;
                    case 'C':
                        playerCanvas.transform.GetChild(3).GetChild(1).GetChild(3).gameObject.SetActive(true);
                        break;
                }
            }
            if (Stack.Length > 4)
            {
                switch (Stack[4])
                {
                    case 'A':
                        playerCanvas.transform.GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(true);
                        break;
                    case 'T':
                        playerCanvas.transform.GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(true);
                        break;
                    case 'G':
                        playerCanvas.transform.GetChild(4).GetChild(1).GetChild(2).gameObject.SetActive(true);
                        break;
                    case 'C':
                        playerCanvas.transform.GetChild(4).GetChild(1).GetChild(3).gameObject.SetActive(true);
                        break;
                }
            }
        }

        void ChangeSprite()
        {
            switch (CurrentState)
            {
                case "A":
                    this.GetComponent<SpriteRenderer>().sprite = StateA;
                    break;
                case "T":
                    this.GetComponent<SpriteRenderer>().sprite = StateT;
                    break;
                case "G":
                    this.GetComponent<SpriteRenderer>().sprite = StateG;
                    break;
                case "C":
                    this.GetComponent<SpriteRenderer>().sprite = StateC;
                    break;
            }
        }

        public void ReceiveState(string state)
        {
            CurrentState = state;
            ChangeSprite();
            //Debug.Log("CurrentState: " + CurrentState + " Sequence " + Sequence + " Sequence[Stack.Length] " + Sequence[Stack.Length]);
            if (Sequence[Stack.Length].Equals(state[0]))
            {
                Stack += state;
                PhotonNetwork.Instantiate("RightSound1", this.transform.position, Quaternion.identity, 0);
                /*
                if (Sequence.Equals(Stack))
                {
                    Debug.Log("You Win!!!");
                    GameObject.FindGameObjectWithTag("WinPanel").transform.GetChild(0).gameObject.SetActive(true);

                    //PhotonNetwork.LeaveRoom();
                    //SceneManager.LoadScene(0);
                }
                */
                SetupSequence();
                UpdateStack();
            }
            else
            {
                Stack = "";
                CleanStackLetters();
            }
        }

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(Sequence);
                stream.SendNext(Stack);
                stream.SendNext(CurrentState);
                //stream.SendNext(_player_name);
                //Debug.Log("Writing!!!");
            }
            else
            {
                Sequence = (string)stream.ReceiveNext();
                Stack = (string)stream.ReceiveNext();
                CurrentState = (string)stream.ReceiveNext();
                //_player_name = (string)stream.ReceiveNext();

                ChangeSprite();

                UpdateStack();
                SetupSequence();
                //Debug.Log(((string)stream.ReceiveNext())[0]);
            }
        }

    }
}
