using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Failure : MonoBehaviour
{
    [SerializeField] Text contentText;

    public void Message(string error)
    {
        error = error.Replace("/Client/LoginWithEmailAddress:", "");

        contentText.text = error;
    }



    public void Confirm()
    {
        gameObject.SetActive(false);
    }
}