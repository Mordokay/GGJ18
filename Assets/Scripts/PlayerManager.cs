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
        public Text _player_label;
        private PlayerInfo info;

        void Start()
        {
            info = GetComponent<PlayerInfo>();
            _player_label.text = info.GetSequence().ToString();

            _player_label = Instantiate(_player_label.gameObject).GetComponent<Text>();
            _player_label.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z);
        }

        void Update()
        {
            _player_label.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z);
            _player_label.text = info.GetSequence().ToString();
        }

    }
}
