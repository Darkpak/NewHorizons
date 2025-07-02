using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class LightFloor : MonoBehaviour
{
    public Material material;
    public void LetItBeLight()
    {
        StartCoroutine(Shazam());
    }

    public IEnumerator Shazam()
    {
        float changeColor=0;
        while (changeColor < 1)
        {
            changeColor += 0.15f;
            material.SetFloat("_EmitToggle", changeColor);
            yield return new WaitForEndOfFrame();
        }
        
    }
    public void ResetSymbols()
    {
        material.SetFloat("_EmitToggle", 0);
    }
    private void OnApplicationQuit()
    {
        ResetSymbols();
    }
}
