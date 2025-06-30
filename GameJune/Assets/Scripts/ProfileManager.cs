using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] GameObject profilePanel;

    private void Awake()
    {
        PlayFabClientAPI.GetAccountInfo
        (
            new GetAccountInfoRequest(),
            Success,
            Failure
        );
    }

    public void Submit()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputField.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName
        (
            request,
            Success,
            Failure
        );
    }

    void Success(UpdateUserTitleDisplayNameResult updateUserTitleDisplayNameResult)
    {
        profilePanel.SetActive(false);
    }

    void Success(GetAccountInfoResult getAccountInfoResult)
    {
        if(string.IsNullOrEmpty(getAccountInfoResult.AccountInfo.TitleInfo.DisplayName))
        {
            profilePanel.SetActive(true);
        }
    }

    void Failure(PlayFabError playFabError)
    {
        Debug.Log(playFabError.GenerateErrorReport());
    }
}
