using UnityEditor;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    [MenuItem("Tools/Combine Selected Meshes")]
    static void Combine()
    {
        GameObject[] selection = Selection.gameObjects;

        if (selection.Length < 2)
        {
            Debug.LogWarning("Select at least two objects to combine.");
            return;
        }

        // Create a new parent object
        GameObject combinedObject = new GameObject("CombinedChains");
        combinedObject.transform.position = Vector3.zero;

        MeshFilter[] meshFilters = new MeshFilter[selection.Length];
        CombineInstance[] combine = new CombineInstance[selection.Length];

        for (int i = 0; i < selection.Length; i++)
        {
            meshFilters[i] = selection[i].GetComponent<MeshFilter>();
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = selection[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combine);

        MeshFilter mf = combinedObject.AddComponent<MeshFilter>();
        mf.mesh = combinedMesh;
        MeshRenderer mr = combinedObject.AddComponent<MeshRenderer>();
        mr.sharedMaterial = selection[0].GetComponent<MeshRenderer>().sharedMaterial;

        Debug.Log("Combined " + selection.Length + " meshes!");
    }
}
