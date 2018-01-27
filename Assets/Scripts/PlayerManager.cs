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
        public Text label;
        void Start()
        {
            info = GetComponent<PlayerInfo>();
            Vector3 label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            label.gameObject.transform.position = label_position;
        }

        void Update()
        {
            Vector3 label_position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            label.gameObject.transform.position = label_position;
            label.text = new string(info.GetSequence().ToArray<char>());
        }

    }
}
