﻿using Assets.Scripts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayerInfo : MonoBehaviour
    {
        private List<char> _sequence;
        private List<char> _stack;

        private char _currentState;
        private System.Random _random_generator;

        public void SetUp()
        {
            _sequence = new List<char>();
            _stack = new List<char>();
            _random_generator = new System.Random();

            int sequence_number;
            for (int i = 0; i < PlayerConsts.SEQUENCE_NUMBER; i ++)
            {
                sequence_number = _random_generator.Next(1, 4);
                char sequence_state = Convert.ToChar(PlayerConsts.SEQUENCE_STATES[sequence_number]);
                _sequence.Add(sequence_state);
            }

            sequence_number = _random_generator.Next(1, 4);
            _currentState = Convert.ToChar(PlayerConsts.SEQUENCE_STATES[sequence_number]);

            string debug_seq = "";
            foreach (char c in _sequence)
            {
                debug_seq += c;
            }
            Debug.Log(debug_seq);
        }

        public char GetState()
        {
            return _currentState;
        }

        public List<char> GetSequence()
        {
            return _sequence;
        }

        public void ReceiveState(char state)
        {
            _currentState = state;

            if (_sequence[_stack.Count + 1] == state)
            {
                _stack.Add(state);
                if (_sequence.Count == _stack.Count)
                {
                    //GAME ENDS THIS PLAYER WINS
                }
            }
            else
            {
                _stack.Clear();
            }
        }
    }
}
