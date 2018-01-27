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

        void Start()
        {
            info = GetComponent<PlayerInfo>();
            info.SetUp();
            Vector3 label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            _sequence_label.gameObject.transform.position = label_position;
        }

        void Update()
        {
            Vector3 label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            _sequence_label.gameObject.transform.position = label_position;
            _sequence_label.text = new string(info.GetSequence().ToArray<char>());
            _state_label.text = info.GetState().ToString();
        }

    }
}
