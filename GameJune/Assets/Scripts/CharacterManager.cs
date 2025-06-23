using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> transformList;

    void Awake()
    {
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;

        PhotonNetwork.Instantiate("Character", transformList[index].position, Quaternion.identity); 
    }    
}
