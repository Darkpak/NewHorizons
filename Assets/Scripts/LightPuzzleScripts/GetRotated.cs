using UnityEngine;

public class GetRotated : MonoBehaviour
{
    private GameObject objectToRotate;
    private ParticleSystem particleSystem;

    public void GetObjectToRotate(GameObject go,ParticleSystem ps)
    {
        if(particleSystem != null)
        {
            particleSystem.gameObject.SetActive(false);
        }
        objectToRotate = go;
        particleSystem = ps;
    }

    public void RotateRight()
    {
        if (objectToRotate != null)
        {
            objectToRotate.transform.Rotate(0, transform.rotation.y - 5, 0);
        }
    }
    public void RotateLeft()
    {
        if (objectToRotate != null)
        {
            objectToRotate.transform.Rotate(0, transform.rotation.y + 5, 0);
        }
    }
}
