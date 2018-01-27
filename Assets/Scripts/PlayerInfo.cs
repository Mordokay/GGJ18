using Assets.Scripts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        private List<char> _sequence;
        private char _currentState;
        private System.Random _random_generator;

        void Start()
        {
            _sequence = new List<char>();
            _random_generator = new System.Random();

            int sequence_number;
            for (int i = 0; i < PlayerConsts.SEQUENCE_NUMBER; i ++)
            {
                sequence_number = _random_generator.Next(65, 90);
                char sequence_state = Convert.ToChar(sequence_number);
                _sequence.Add(sequence_state);
            }

            string debug_seq = "";
            foreach (char c in _sequence)
            {
                debug_seq += c;
            }
            Debug.Log(debug_seq);
        }

        public List<char> GetSequence()
        {
            return _sequence;
        }
    }
}
