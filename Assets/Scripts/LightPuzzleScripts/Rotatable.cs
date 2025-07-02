using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public GetRotated getRotated;
    public ParticleSystem particleSystem;
    public bool interactable = true;
    public GameObject glass;

    private void OnMouseDown()
    {
        if (interactable)
        {
            particleSystem.gameObject.SetActive(true);
            getRotated.GetObjectToRotate(glass, particleSystem);
        }       

    }
}
