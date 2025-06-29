using UnityEngine;

public class TriggerErosion : MonoBehaviour
{
    public ErosionManipulator erosion;

    public void TriggerErosionTarget()
    {
        if (erosion != null)
        {
            erosion.DestroyErosion();
        }
    }
}
