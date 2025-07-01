using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerErosion : MonoBehaviour
{
    public ErosionManipulator erosion;

    public UnityEvent onActivated;

    public void TriggerErosionTarget()
    {
        onActivated.Invoke();
    }
}
