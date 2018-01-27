using UnityEngine;
using System.Collections;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform[] SpawnPositions;
    public GameObject[] PrefabsToInstantiate;

    public void OnJoinedRoom()
    {
        if (this.PrefabsToInstantiate != null)
        {
            foreach (GameObject o in this.PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = Vector3.zero;
                if (this.SpawnPositions.Length > 0)
                {
                    spawnPos = this.SpawnPositions[Random.Range(0, SpawnPositions.Length)].position;
                }

<<<<<<< HEAD
                PhotonNetwork.Instantiate(name, spawnPos, Quaternion.identity, 0);
=======
                GameObject myPlayer = PhotonNetwork.Instantiate(o.name, spawnPos, Quaternion.identity, 0);
                Camera.main.GetComponent<CameraFollowPlayer>().player = myPlayer;
>>>>>>> 3c67f6b578b9528fe079e53ba2017200626c1a45
            }
        }
    }
}
