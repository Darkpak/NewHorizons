using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameras : MonoBehaviour
{

    public List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    public static PlayerCameras instance;

    void Start()
    {
        instance = this;
    }

    public void Prioritize(CinemachineCamera camera)
    {
        camera.Priority = 20;
    }
}
