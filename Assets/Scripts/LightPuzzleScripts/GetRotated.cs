using UnityEngine;

public class GetRotated : MonoBehaviour
{
    private GameObject objectToRotate;

    public void GetObjectToRotate(GameObject go)
    {
        objectToRotate = go;
    }

    public void RotateRight()
    {
        if (objectToRotate != null)
        {
            objectToRotate.transform.Rotate(0, transform.rotation.y - 15, 0);
        }
    }
    public void RotateLeft()
    {
        if (objectToRotate != null)
        {
            objectToRotate.transform.Rotate(0, transform.rotation.y + 15, 0);
        }
    }
}
