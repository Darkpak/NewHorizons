using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Elements")]
    public List<PuzzleTile> innerTiles = new List<PuzzleTile>(12);
    public List<Transform> innerPositions = new List<Transform>(12);
    public OuterRingTile[] outerTiles;
    public Transform innerCircle;

    [Header("Interaction Points")]
    public List<InvisibleOuterTileChecker> interactionCheckers;
    public List<Transform> pocketSlots;
    private PuzzleTile[] pocketSlotsContent;
    private bool isPuzzleSolved = false;

    void Awake()
    {
        if (pocketSlots != null)
        {
            pocketSlotsContent = new PuzzleTile[pocketSlots.Count];
        }
    }

    void Start()
    {
        CheckAlignment();
    }

    public void CheckAlignment()
    {
        // Safety check to ensure all required lists are assigned.
        if (
            innerCircle == null
            || outerTiles == null
            || outerTiles.Length == 0
            || innerPositions.Count == 0
        )
            return;

        float rotationY = innerCircle.localEulerAngles.y;
        int offset = Mathf.RoundToInt(rotationY / 30f) % innerPositions.Count;
        if (offset < 0)
            offset += innerPositions.Count;

        HashSet<PuzzleTile> matchedTilesThisFrame = new HashSet<PuzzleTile>();

        for (int i = 0; i < outerTiles.Length; i++)
        {
            int rotatedIndex = (i + offset) % innerPositions.Count;
            if (rotatedIndex < 0)
                rotatedIndex += innerPositions.Count;

            if (rotatedIndex >= 0 && rotatedIndex < innerTiles.Count)
            {
                PuzzleTile innerTile = innerTiles[rotatedIndex];
                OuterRingTile outerTile = outerTiles[i];

                if (innerTile != null && !innerTile.IsExtracted)
                {
                    if (outerTile.runeID != 15 && innerTile.runeID == outerTile.runeID)
                    {
                        matchedTilesThisFrame.Add(innerTile);
                    }
                }
            }
        }

        for (int i = 0; i < innerTiles.Count; i++)
        {
            PuzzleTile tile = innerTiles[i];
            if (tile != null && !tile.IsExtracted)
            {
                tile.SetMatchState(matchedTilesThisFrame.Contains(tile));
            }
        }
    }

    #region Core Logic
    public Transform GetInsertionTargetForPocket(int pocketIndex)
    {
        InvisibleOuterTileChecker targetChecker = null;
        foreach (var checker in interactionCheckers)
        {
            if (checker.correspondingPocketIndex == pocketIndex)
            {
                targetChecker = checker;
                break;
            }
        }
        if (targetChecker != null)
        {
            int alignedSlotIndex = GetAlignedInnerIndex(targetChecker.myOuterIndex);
            if (
                alignedSlotIndex != -1
                && alignedSlotIndex < innerTiles.Count
                && innerTiles[alignedSlotIndex] == null
            )
            {
                return innerPositions[alignedSlotIndex];
            }
        }
        return null;
    }

    public void InsertInnerTile(PuzzleTile tile, int index)
    {
        if (index < 0 || index >= innerTiles.Count)
            return;
        innerTiles[index] = tile;
        tile.transform.SetParent(innerCircle);
    }

    public int GetTargetPocketForExtraction(int tileToExtractIndex)
    {
        foreach (var checker in interactionCheckers)
        {
            int alignedSlot = GetAlignedInnerIndex(checker.myOuterIndex);
            if (tileToExtractIndex == alignedSlot)
            {
                if (
                    checker.correspondingPocketIndex < pocketSlotsContent.Length
                    && pocketSlotsContent[checker.correspondingPocketIndex] == null
                )
                {
                    return checker.correspondingPocketIndex;
                }
            }
        }
        return -1;
    }

    public int GetAlignedInnerIndex(int outerIndex)
    {
        if (innerCircle == null || innerPositions.Count == 0)
            return -1;
        float rotationY = innerCircle.localEulerAngles.y;
        int offset = Mathf.RoundToInt(rotationY / 30f) % innerPositions.Count;
        int rotatedIndex = (outerIndex + offset) % innerPositions.Count;
        if (rotatedIndex < 0)
            rotatedIndex += innerPositions.Count;
        return rotatedIndex;
    }

    public void RemoveInnerTile(PuzzleTile tile)
    {
        int index = innerTiles.IndexOf(tile);
        if (index != -1)
        {
            innerTiles[index] = null;
        }
    }

    public Transform GetPocketSlotTransform(int index) =>
        (index >= 0 && index < pocketSlots.Count) ? pocketSlots[index] : null;

    public void OnTilePlacedInPocket(PuzzleTile tile, int pocketIndex)
    {
        if (pocketIndex >= 0 && pocketIndex < pocketSlotsContent.Length)
        {
            pocketSlotsContent[pocketIndex] = tile;
        }
        CheckAlignment();
    }

    public void OnTileRemovedFromPocket(int pocketIndex)
    {
        if (pocketIndex >= 0 && pocketIndex < pocketSlotsContent.Length)
        {
            pocketSlotsContent[pocketIndex] = null;
        }
    }
    #endregion
    public void CheckWin()
    {
        if (isPuzzleSolved)
        {
            return;
        }

        // 1. First, check if any tiles are still in the pockets. If so, not solved.
        foreach (var tile in pocketSlotsContent)
        {
            if (tile != null) return;
        }

        // 2. Then, check if all inner slots are full. If any are empty, not solved.
        foreach (var tile in innerTiles)
        {
            if (tile == null) return;
        }

        // 3. Finally, check if every single tile is correctly aligned.
        float rotationY = innerCircle.localEulerAngles.y;
        int offset = Mathf.RoundToInt(rotationY / 30f) % innerPositions.Count;
        if (offset < 0) offset += innerPositions.Count;

        for (int i = 0; i < outerTiles.Length; i++)
        {
            int rotatedIndex = (i + offset) % innerPositions.Count;
            if (rotatedIndex < 0) rotatedIndex += innerPositions.Count;

            PuzzleTile innerTile = innerTiles[rotatedIndex];
            OuterRingTile outerTile = outerTiles[i];

            // If we find even one mismatch, the puzzle is not solved.
            if (innerTile.runeID != outerTile.runeID)
            {
                return;
            }
        }

        // If we get through all the checks without returning, the puzzle is solved!
        OnPuzzleSolved();
    }

    private void OnPuzzleSolved()
    {
        isPuzzleSolved = true;
        Debug.LogWarning("--- PUZZLE SOLVED! ---");
        // This is where you will trigger your animator, open a door, play a sound, etc.
        // For example: myAnimator.SetTrigger("OnSolve");
    }
}
