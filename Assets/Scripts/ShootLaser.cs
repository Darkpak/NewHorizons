using UnityEngine;

public class ShootLaser : MonoBehaviour
{

    public Material material;
    LaserBeam beam;

    // Update is called once per frame
    void Update()
    {
        var oldBeam = GameObject.Find("Laser Beam");
        if (oldBeam != null)
            Destroy(oldBeam);

        beam = new LaserBeam(transform.position, transform.right, material);

    }
}
