using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerErosion : MonoBehaviour
{
    public ErosionManipulator erosion;
    [SerializeField] CinemachineCamera totemCam;
    [SerializeField] CinemachineCamera rootsCam;
    [SerializeField] CinemachineCamera holeCam;

    public void TriggerErosionTarget()
    {
        StartCoroutine(Sequence());

    }

    IEnumerator Sequence()
    { 
        //totem camera
        PlayerCameras.instance.Prioritize(totemCam);
        yield return new WaitForSeconds(3.5f);
        //roots camera
        PlayerCameras.instance.Prioritize(rootsCam);
        yield return new WaitForSeconds(2f);
        //root disapear
        if (erosion != null)
        {
            erosion.DestroyErosion();
        }
        yield return new WaitForSeconds(2.5f);
        //mirror camera
        PlayerCameras.instance.Prioritize(holeCam);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Puzzle_03");

    }
}
