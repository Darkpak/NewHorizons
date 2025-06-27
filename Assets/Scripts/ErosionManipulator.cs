using UnityEngine;

public class ErosionManipulator : MonoBehaviour
{
    public Material erosionMaterial;
    public bool destroyChains;
    public float reveal;

    public PlaySoundEffect soundEvent;
    private bool soundTriggered = false;
    // Update is called once per frame
    void Update()
    {
        if (destroyChains&& reveal>0)
        {  
            erosionMaterial.SetFloat("_Erosion", reveal);
            reveal -= 0.3f*Time.deltaTime;
            if (!soundTriggered)
            {
                soundEvent.PlaySound();
                soundTriggered = true;
            }
        }
        
    }

    public void DestroyErosion()
    {
        destroyChains = true;
    }
    private void OnApplicationQuit()
    {
        erosionMaterial.SetFloat("_Erosion", 1);
    }
}
