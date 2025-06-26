using UnityEngine;

public class InvisibleOuterTileChecker : MonoBehaviour
{
    [Tooltip("The index on the outer ring (0-11) where this checker is placed.")]
    public int myOuterIndex;

    [Tooltip("The pocket this checker corresponds to (0 for the left pocket, 1 for the right).")]
    public int correspondingPocketIndex;
}
