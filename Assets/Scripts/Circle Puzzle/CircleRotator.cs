using System.Collections;
using UnityEngine;

public class CircleRotator : MonoBehaviour
{
    public PuzzleManager puzzleManager; // Assign this in the Inspector
    public PlaySoundEffect soundEffect; // Assign this in the Inspector                     //Ico

    [Tooltip("How long the rotation animation takes in seconds.")]
    public float rotationDuration = 0.3f;

    private bool isRotating = false;
    private float targetYRotation = 0f;
    private const float rotationStep = 30f;

    private void Start()
    {
        float currentY = transform.localEulerAngles.y;
        targetYRotation = currentY + 15f;
    }

    public void RotateRight()
    {
        // Ignore if already rotating
        if (isRotating)
            return;

        targetYRotation += rotationStep;
        //Ico sound addition                                                               //Ico
        soundEffect.PlaySound();
        StartCoroutine(SmoothRotateTo(targetYRotation));
    }

    public void RotateLeft()
    {
        // Ignore if already rotating
        if (isRotating)
            return;

        targetYRotation -= rotationStep;
        //Ico sound addition                                                               //Ico
        soundEffect.PlaySound();
        StartCoroutine(SmoothRotateTo(targetYRotation));
    }

    private IEnumerator SmoothRotateTo(float targetAngle)
    {
        isRotating = true;

        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(0, targetAngle - 15, 0);
        float time = 0;

        while (time < rotationDuration)
        {
            transform.localRotation = Quaternion.Slerp(
                startRotation,
                endRotation,
                time / rotationDuration
            );
            time += Time.deltaTime;
            // Check alignment during rotation for real-time feedback
            if (puzzleManager != null)
                puzzleManager.CheckAlignment();
                puzzleManager.CheckWin();
            yield return null;
        }

        transform.localRotation = endRotation;
        // Final alignment check
        if (puzzleManager != null)
            puzzleManager.CheckAlignment();

        isRotating = false;
    }
}
