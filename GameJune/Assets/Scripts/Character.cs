using Photon.Pun;
using UnityEngine;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;

    [SerializeField] Camera virtualCamera;
    [SerializeField] CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        DisableCamera();
    }
    
    void Update()
    {
        if(photonView.IsMine)
        {
            Control();

            Move();
        }
    }

    public void Control()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        direction.Normalize();
    }

    public void Move()
    {
        characterController.Move(direction * speed * Time.deltaTime);
    }

    public void DisableCamera()
    {
        // 현재 플레이어가 나 자신이라면
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }

}
