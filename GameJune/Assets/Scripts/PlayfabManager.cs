using PlayFab;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using System.Collections;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string version;

    [SerializeField] GameObject failurePanel;
    [SerializeField] InputField emailInputField;
    [SerializeField] InputField passwordInputField;

    public void Success(LoginResult loginResult)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = version;

        StartCoroutine(Connect());
    }

    IEnumerator Connect()
    {
        // Name Server에서 자동에서 Master Server로 연결
        PhotonNetwork.ConnectUsingSettings();

        // 서버 연결이 완료되거나 시간 초과될 떄 까지 대기
        while(PhotonNetwork.IsConnectedAndReady == false)
        {
            yield return null;
        }

        // 특정 로비를 생성하여 진입하는 함수  
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void Access()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputField.text,
            Password = passwordInputField.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            Success,
            Failure
        );
    }

    void Failure(PlayFabError playFabError)
    {
        failurePanel.GetComponent<Failure>().Message(playFabError.GenerateErrorReport());

        failurePanel.SetActive(true);
    }
}
