using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;

    private LaserBeam beam;

    void Start()
    {
        beam = new LaserBeam(material);
    }

    void Update()
    {
        beam.UpdateBeam(transform.position, transform.right);
    }
}
