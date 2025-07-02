using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameras : MonoBehaviour
{

    public List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    public static PlayerCameras instance;

    [SerializeField] CinemachineCamera prioritizableCamera;

    void Start()
    {
        instance = this;
    }

    public void Prioritize(CinemachineCamera camera)
    {
        foreach (CinemachineCamera c in cameras)
        {
            c.Priority = 0;
        }
        camera.Priority = 20;
    }

    private void Awake()
    {
        if (prioritizableCamera != null)
        { 
            PrioritizeOnAwake(prioritizableCamera);
        }
    }

    public void PrioritizeOnAwake(CinemachineCamera camera)
    {
        StartCoroutine(PrioritizeOnAwakeEnumerator(camera));
    }

    IEnumerator PrioritizeOnAwakeEnumerator(CinemachineCamera camera)
    {
        yield return new WaitForSeconds(3f);
        camera.Priority = 0;

    }


}
