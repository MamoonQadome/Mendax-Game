using UnityEngine;
using Cinemachine;
public class CameraSnap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<CinemachineVirtualCamera>().LookAt = player;
        GetComponent<CinemachineVirtualCamera>().Follow = player;
    }

}
