using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public GetRotated getRotated;
    public ParticleSystem particleSystem;
    public bool interactable = true;

    private void OnMouseDown()
    {
        if (interactable)
        {
            particleSystem.gameObject.SetActive(true);
            getRotated.GetObjectToRotate(gameObject, particleSystem);
        }       

    }
}
