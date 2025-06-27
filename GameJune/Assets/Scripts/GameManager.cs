using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;

    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int milliSecond;

    [SerializeField] Text timeText;

    void Start()
    {
        initializeTime = PhotonNetwork.Time;
    }

    void Update()
    {
        time = PhotonNetwork.Time - initializeTime;

        minute = (int)time / 60;
        second = (int)time % 60;
        milliSecond = (int)(time * 100) % 100;

        timeText.text = $"{minute:D2} : {second:D2} : {milliSecond:D2}";
    }
}
