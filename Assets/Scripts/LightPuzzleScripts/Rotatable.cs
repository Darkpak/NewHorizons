using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public GetRotated getRotated;
    public ParticleSystem particleSystem;

    private void OnMouseDown()
    {
        particleSystem.gameObject.SetActive(true);
        getRotated.GetObjectToRotate(gameObject,particleSystem);

    }
}
