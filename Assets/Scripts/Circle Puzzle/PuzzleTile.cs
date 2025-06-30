using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PuzzleTile : MonoBehaviour
{
    public int runeID;
    public PuzzleManager manager;

    [Header("Emission Settings")]
    [Tooltip("The color of the glow when the tile is matched.")]
    public Color matchedEmissionColor = new Color(1.0f, 0.75f, 0.3f); // Default to a warm orange glow

    [Tooltip("How bright the emission glow is. Requires a Bloom effect to be visible.")]
    [Range(0f, 10f)]
    public float emissionIntensity = 2.5f;

    private MeshRenderer rend;
    private bool isExtracted = false;

    public bool IsExtracted => isExtracted;

    private static bool IsAnyTileMoving = false;
    private int myPocketIndex = -1;
    private Transform insertionTarget;

    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (isExtracted && manager != null)
        {
            insertionTarget = manager.GetInsertionTargetForPocket(myPocketIndex);
            if (insertionTarget != null)
            {
                Debug.DrawLine(transform.position, insertionTarget.position, Color.white);
            }
        }

        if (Input.GetMouseButtonDown(0) && !IsAnyTileMoving && manager != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                HandleClick();
            }
        }
    }

    private void HandleClick()
    {
        if (!isExtracted)
        {
            int myIndex = manager.innerTiles.IndexOf(this);
            if (myIndex == -1)
                return;

            int targetPocketIndex = manager.GetTargetPocketForExtraction(myIndex);
            if (targetPocketIndex != -1)
            {
                ExtractTile(targetPocketIndex);
            }
        }
        else
        {
            if (insertionTarget != null)
            {
                TryReinsert(insertionTarget);
            }
        }
    }

    void ExtractTile(int targetPocketIndex)
    {
        myPocketIndex = targetPocketIndex;
        Transform targetPocket = manager.GetPocketSlotTransform(targetPocketIndex);
        if (targetPocket == null)
            return;

        manager.RemoveInnerTile(this);
        transform.SetParent(null);

        StartCoroutine(
            AnimateToTransform(
                targetPocket.position,
                () =>
                {
                    isExtracted = true;
                    manager.OnTilePlacedInPocket(this, myPocketIndex);
                }
            )
        );
    }

    void TryReinsert(Transform ignoredTarget)
    {
        manager.OnTileRemovedFromPocket(myPocketIndex);
        myPocketIndex = -1;

        int targetIndex = manager.innerPositions.IndexOf(ignoredTarget);
        if (targetIndex == -1)
            return;

        Transform realTarget = manager.innerPositions[targetIndex];

        StartCoroutine(AnimateOnReinsert(realTarget, targetIndex));
    }

    IEnumerator AnimateOnReinsert(Transform slotTransform, int slotIndex)
    {
        IsAnyTileMoving = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = slotTransform.position;
        float duration = 0.2f;
        float time = 0f;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;

        isExtracted = false;
        manager.InsertInnerTile(this, slotIndex);
        transform.SetParent(manager.innerCircle);
        transform.localPosition = slotTransform.localPosition;
        transform.localScale = slotTransform.localScale;

        manager.CheckAlignment();
        manager.CheckWin();
        IsAnyTileMoving = false;
    }


    IEnumerator AnimateToTransform(Vector3 targetPos, System.Action onComplete)
    {
        IsAnyTileMoving = true;
        float duration = 0.2f;
        float time = 0f;
        Vector3 startPos = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        onComplete?.Invoke();
        IsAnyTileMoving = false;
    }

    public void SetMatchState(bool matched)
    {
        if (rend == null) return;

        if (matched)
        {
            Color finalEmission = matchedEmissionColor * emissionIntensity;
            rend.material.SetColor("_EmissionColor", finalEmission);
        }
        else
        {
            rend.material.SetColor("_EmissionColor", Color.black);
        }

    }

}
