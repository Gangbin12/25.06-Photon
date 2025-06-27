using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 direction;
    [SerializeField] WaitForSeconds waitForSeconds = new WaitForSeconds(5.0f);

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(Create());   
        }
    }

    public IEnumerator Create()
    {
        while(true)
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                PhotonNetwork.InstantiateRoomObject("Unit", direction, Quaternion.identity);
            }

            yield return waitForSeconds;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log(newMasterClient);

        PhotonNetwork.SetMasterClient(newMasterClient);
    }
}
