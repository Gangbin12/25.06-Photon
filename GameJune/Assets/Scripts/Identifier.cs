using Photon.Pun;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.UI;

public class Identifier : MonoBehaviourPunCallbacks
{
    [SerializeField] Text nameText;

    private void Awake()
    {
        Load();
    }

    void Load()
    {
        PlayFabClientAPI.GetAccountInfo
        (
            new GetAccountInfoRequest(),
            Success,
            Failure
        );
    }

    void Success(GetAccountInfoResult getAccountInfoResult)
    {
        PhotonNetwork.LocalPlayer.NickName = getAccountInfoResult.AccountInfo.TitleInfo.DisplayName;

        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
        }
    }

    void Failure(PlayFabError playFabError)
    {
        Debug.Log(playFabError.GenerateErrorReport());
    }


}