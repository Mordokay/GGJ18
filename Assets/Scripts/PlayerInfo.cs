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
        public List<char> Sequence { get; set; }
        public List<char> Stack { get; set; }

        public char CurrentState { get; set; }
        private System.Random _random_generator;

        public void SetUp()
        {
            Sequence = new List<char>();
            Stack = new List<char>();
            _random_generator = new System.Random();

            int sequence_number;
            for (int i = 0; i < PlayerConsts.SEQUENCE_NUMBER; i ++)
            {
                sequence_number = _random_generator.Next(0, 4);
                char sequence_state = Convert.ToChar(PlayerConsts.SEQUENCE_STATES[sequence_number]);
                Sequence.Add(sequence_state);
            }

            sequence_number = _random_generator.Next(0, 4);
            CurrentState = Convert.ToChar(PlayerConsts.SEQUENCE_STATES[sequence_number]);

            string debug_seq = "";
            foreach (char c in Sequence)
            {
                debug_seq += c;
            }
            Debug.Log(debug_seq);
        }

        public void ReceiveState(char state)
        {
            CurrentState = state;

            if (Sequence[Stack.Count] == state)
            {
                Stack.Add(state);
                if (Sequence.Count == Stack.Count)
                {
                    //GAME ENDS THIS PLAYER WINS
                }
            }
            else
            {
                Stack.Clear();
            }
        }
    }
}
