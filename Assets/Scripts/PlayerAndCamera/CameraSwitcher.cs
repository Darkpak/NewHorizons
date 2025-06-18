using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public static class CameraSwitcher
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    static CinemachineCamera currentCamera;


    public static void RegisterCamera(CinemachineCamera camera)
    {
        Debug.Log($"Camera: {camera} registered!");
        cameras.Add(camera);

    }
    public static void UnregisterCamera(CinemachineCamera camera)
    {
        Debug.Log($"Camera: {camera} unregistered!");
        cameras.Remove(camera);
    }

    public static void SetInitialCamera(CinemachineCamera camera)
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }

        currentCamera = camera;
        currentCamera.Priority = 10;
    }

    public static void SwitchCamera(CinemachineCamera newCamera)
    {
        if (currentCamera == newCamera)
            return;

        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }

        currentCamera = newCamera;
        currentCamera.Priority = 10;

        Debug.Log($"Switched to camera: {newCamera.name}");
    }

}
