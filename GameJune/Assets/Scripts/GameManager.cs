using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;

    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int milliSecond;

    [SerializeField] Text timeText;

    [SerializeField] GameObject pausePanel;

    void Start()
    {
        if (photonView.IsMine)
        {
            SetMouse(false);
        }

        if(PhotonNetwork.IsMasterClient)
        {
            initializeTime = PhotonNetwork.Time;

            photonView.RPC("InitializeTime", RpcTarget.AllBuffered, initializeTime);
        }
    }

    [PunRPC]

    void InitializeTime(double time)
    {
        initializeTime = time;
    }

    public void SetMouse(bool state)
    {
        if (photonView.IsMine)
        {
            Cursor.visible = state;

            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    void Update()
    {
        time = PhotonNetwork.Time - initializeTime;

        minute = (int)time / 60;
        second = (int)time % 60;
        milliSecond = (int)(time * 100) % 100;

        timeText.text = $"{minute:D2} : {second:D2} : {milliSecond:D2}";

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetMouse(true);

                pausePanel.SetActive(true);
            }
        }
    }

    public void Continue()
    {
        if (photonView.IsMine)
        {
            SetMouse(false);

            pausePanel.SetActive(false);
        }
    }

    public void Exit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            SetMouse(true);
        }
    }

}
