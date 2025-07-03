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
        SetMouse(false);

        initializeTime = PhotonNetwork.Time;
    }

    public void SetMouse(bool state)
    {
        if (photonView.IsMine)
        {
            Cursor.visible = state;

            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
        }
    }

    void Update()
    {
        time = PhotonNetwork.Time - initializeTime;

        minute = (int)time / 60;
        second = (int)time % 60;
        milliSecond = (int)(time * 100) % 100;

        timeText.text = $"{minute:D2} : {second:D2} : {milliSecond:D2}";

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetMouse(true);

            pausePanel.SetActive(true);
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
        SetMouse(true);
    }

}
