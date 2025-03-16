using Unity.Cinemachine;
using UnityEngine;

public class VCamInitializor : MonoBehaviour
{

    void Start()
    {
        Player player = FindFirstObjectByType<Player>();
        CinemachineCamera vCam = gameObject.GetComponent<CinemachineCamera>();

        vCam.Follow = player.gameObject.transform;
        vCam.LookAt = player.gameObject.transform;
    }

    
}
