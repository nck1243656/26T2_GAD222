using Unity.Cinemachine;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineCamera thirdPersonCam;
    [SerializeField] private CinemachineCamera staticCam;

    [Header("Player References")]
    [SerializeField] private Transform playerObj;
    [SerializeField] private PlayerMovement playerMovement;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (playerMovement.isThirdPersonCamActive)
        {
            Debug.Log("Switching to static camera");

            playerMovement.isThirdPersonCamActive = false;

            thirdPersonCam.Priority = 0;
            staticCam.Priority = 1;
        }
        else
        {
            Debug.Log("Switching to third person camera");

            playerMovement.isThirdPersonCamActive = true;

            thirdPersonCam.Priority = 1;
            staticCam.Priority = 0;
        }
    }

}
