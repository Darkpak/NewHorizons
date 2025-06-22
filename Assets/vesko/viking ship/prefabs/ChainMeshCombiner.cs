#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ChainMeshCombiner
{
    [MenuItem("Tools/Combine Selected Chains Into One Mesh")]
    static void CombineSelectedChains()
    {
        GameObject[] selected = Selection.gameObjects;

        if (selected.Length == 0)
        {
            Debug.LogWarning("No objects selected. Please select your chain pieces in the Hierarchy.");
            return;
        }

        CombineInstance[] combine = new CombineInstance[selected.Length];
        Material sharedMat = null;

        for (int i = 0; i < selected.Length; i++)
        {
            MeshFilter mf = selected[i].GetComponent<MeshFilter>();
            MeshRenderer mr = selected[i].GetComponent<MeshRenderer>();

            if (mf == null || mf.sharedMesh == null)
            {
                Debug.LogWarning("Object " + selected[i].name + " has no mesh filter or mesh.");
                return;
            }

            if (i == 0 && mr != null)
                sharedMat = mr.sharedMaterial;

            combine[i].mesh = mf.sharedMesh;
            combine[i].transform = selected[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combine);

        // Create new GameObject to hold combined mesh
        GameObject combinedGO = new GameObject("Combined_Chain");
        MeshFilter newMF = combinedGO.AddComponent<MeshFilter>();
        MeshRenderer newMR = combinedGO.AddComponent<MeshRenderer>();
        newMF.sharedMesh = combinedMesh;
        newMR.sharedMaterial = sharedMat;

        // Save mesh as asset
        string path = "Assets/CombinedChainMesh.asset";
        AssetDatabase.CreateAsset(combinedMesh, path);
        AssetDatabase.SaveAssets();

        Debug.Log("✅ Combined " + selected.Length + " meshes into one. Saved to: " + path);
    }
}
#endif
