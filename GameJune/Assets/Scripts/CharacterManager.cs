using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 direction;

    void Start()
    {
        PhotonNetwork.Instantiate("Character", direction, Quaternion.identity);    
    }
}
