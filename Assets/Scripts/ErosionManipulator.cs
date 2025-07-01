using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ErosionManipulator : MonoBehaviour
{
    public Material erosionMaterial;
    public bool destroyChains;
    public float reveal;

    public UnityEvent onDestroyed;

    public PlaySoundEffect soundEvent;
    private bool soundTriggered = false;
    // Update is called once per frame
    void Update()
    {
        if (destroyChains && reveal > 0)
        {
            erosionMaterial.SetFloat("_Erosion", reveal);
            reveal -= 0.3f * Time.deltaTime;
            if (!soundTriggered)
            {
                soundEvent.PlaySound();
                soundTriggered = true;
            }
            StartCoroutine(OnDestroyed());
        }        
    }

    public void DestroyErosion()
    {
        destroyChains = true;
    }
    private void OnApplicationQuit()
    {
        FixErosionOnExit();
    }

    public void FixErosionOnExit()
    {
        erosionMaterial.SetFloat("_Erosion", 1);
    }

    IEnumerator OnDestroyed()
    { 
        yield return new WaitForSeconds(2.5f); 
        onDestroyed.Invoke();
    }
}
