using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Information : MonoBehaviourPunCallbacks
{
    [SerializeField] string roomName;

    [SerializeField] Text titleText;

    public void OnConnectRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void Details(string name, int currentStaff, int maxStaff)
    {
        roomName = name;

        titleText.text = roomName + " ( " + currentStaff + " / " + maxStaff + " ) ";
    }
}
