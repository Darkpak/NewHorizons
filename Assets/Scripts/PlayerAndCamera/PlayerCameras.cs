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

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        foreach (CinemachineCamera cam in cameras)
        {
            CameraSwitcher.RegisterCamera(cam);
        }
    }
    private void OnDisable()
    {
        foreach(CinemachineCamera cam in cameras)
        {
            CameraSwitcher.UnregisterCamera(cam); 
        }
    }
}
