using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTriggerObject : MonoBehaviour
{
    [SerializeField] CinemachineCamera targetCamera;
    [SerializeField] List<GameObject> triggers = new List<GameObject>();

    [SerializeField] string sceneToLoad;
    [SerializeField] float delay;

    private IEnumerator OnMouseDown()
    {
        if (targetCamera != null)
        {
            Debug.Log($"Going to {targetCamera}");
            CameraSwitcher.SwitchCamera(targetCamera);
            if (sceneToLoad != null)
            { 
                yield return new WaitForSeconds(delay);
                SceneManager.LoadScene(sceneToLoad);
            }
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"Tupanar");
        }
    }

    public void GoToInitialCamera()
    {
        PlayerCameras.instance.cameras[0].Priority = 10;
        foreach (var t in triggers)
        {
            t.SetActive(true);
        }
        foreach (CinemachineCamera camera in PlayerCameras.instance.cameras)
        {
            if (camera != PlayerCameras.instance.cameras[0])
            {
                camera.Priority = 0;
            }
        }
    }
}
