using UnityEngine;

public class ErosionManipulator : MonoBehaviour
{
    public Material erosionMaterial;
    public bool destroyChains;
    public float reveal; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyChains&& reveal>0)
        {
            erosionMaterial.SetFloat("_Erosion", reveal);
            reveal -= 0.3f*Time.deltaTime;
        }
        
    }
}
