using Photon.Pun;
using System;
using UnityEngine;

public class Mouse : MonoBehaviourPun
{
    void Start()
    {
        SetMouse(false);
    }

    public void SetMouse(bool state)
    {
        if(photonView.IsMine)
        {
            Cursor.visible = state;

            Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
        }
    }

    private void OnDestroy()
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
    }

}
