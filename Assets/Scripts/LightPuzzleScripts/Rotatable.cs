using UnityEngine;

public class Rotatable : MonoBehaviour
{
    public GetRotated getRotated;

    private void OnMouseDown()
    {
        getRotated.GetObjectToRotate(gameObject);
    }
}
