using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PuzzleTile : MonoBehaviour
{
    public int runeID;
    public Material normalMat;
    public Material matchedMat;
    public PuzzleManager manager;

    private Renderer rend;
    private bool isExtracted = false;

    public bool IsExtracted => isExtracted;

    private static bool IsAnyTileMoving = false;
    private int myPocketIndex = -1;
    private Transform insertionTarget;

    void Awake()
    {
        rend = GetComponent<Renderer>();
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

    void TryReinsert(Transform targetTransform)
    {
        manager.OnTileRemovedFromPocket(myPocketIndex);
        myPocketIndex = -1;

        int targetIndex = manager.innerPositions.IndexOf(targetTransform);
        if (targetIndex == -1)
            return;

        StartCoroutine(
            AnimateToTransform(
                targetTransform.position,
                () =>
                {
                    isExtracted = false;
                    manager.InsertInnerTile(this, targetIndex);

                    // guarantee a perfect fit.
                    transform.SetParent(manager.innerCircle);
                    transform.localPosition = targetTransform.localPosition;
                    transform.localScale = targetTransform.localScale;

                    manager.CheckAlignment();
                    manager.CheckWin();
                }
            )
        );
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
        if (rend != null && normalMat != null && matchedMat != null)
        {
            rend.material = matched ? matchedMat : normalMat;
        }
    }
}
