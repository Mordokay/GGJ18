using Assets.Scripts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerInfo : MonoBehaviour
    {
        public string Sequence { get; set; }
        public string Stack { get; set; }

        public string CurrentState;

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
        }

        public void ReceiveState(string state)
        {
            CurrentState = state;
            Debug.Log("CurrentState: " + CurrentState + " Sequence " + Sequence + " Sequence[Stack.Length] " + Sequence[Stack.Length]);
            if (Sequence[Stack.Length].Equals(state[0]))
            {
                Debug.Log("fuck yeah!!!");
                Stack += state;
                if (Sequence.Equals(Stack))
                {
                    //GAME ENDS THIS PLAYER WINS
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
                //Debug.Log(((string)stream.ReceiveNext())[0]);
            }
        }

    }
}
