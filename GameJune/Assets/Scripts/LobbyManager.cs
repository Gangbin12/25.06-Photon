using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform parentTransform;

    [SerializeField] Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();


    public void OnCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 4;

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom("Battle", roomOptions);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach (RoomInfo room in roomList)
        {
            // 룸 삭제 경우
            if(room.RemovedFromList == true)
            {
                dictionary.TryGetValue(room.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(room.Name);
            }
            else // 룸 정보 변경 경우
            {
                // 룸이 처음 생성 되었을때
                if(dictionary.ContainsKey(room.Name) == false)
                {
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), parentTransform);

                    clone.GetComponent<Information>().Details(room.Name, room.PlayerCount, room.MaxPlayers);

                    dictionary.Add(room.Name, clone);
                }
                else // 룸이 갱신 되었을때
                {
                    dictionary.TryGetValue(room.Name, out prefab);

                    prefab.GetComponent<Information>().Details(room.Name, room.PlayerCount, room.MaxPlayers);
                }
            }
        }
    }


}
