using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] float rotationSpeed;
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
            // UI에 포커스가 있다면 입력을 무시합니다.
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            Control();

            Move();

            Rotate();
        }
    }

    public void Control()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        direction.Normalize();

        // mouseX에 마우스로 입력한 값을 저장합니다.
        mouseX += Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime;
    }

    public void Move()
    {
        // 방향 * 속도 * 시간
        characterController.Move(characterController.transform.TransformDirection(direction) * speed * Time.deltaTime);
    }

    public void Rotate()
    {
        transform.eulerAngles = new Vector3(0,mouseX,0);
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

            virtualCamera.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Authorized"))
        {
            PhotonView clone = other.GetComponent<PhotonView>();

            if(clone.IsMine)
            {
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }
}
