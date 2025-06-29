using System.Collections.Generic;
using UnityEngine;

public class LaserBeam 
{
    private Vector3 pos, dir;
    private GameObject laserObj;
    private LineRenderer laser;
    private List<Vector3> laserPoints;

    public LaserBeam(Material material)
    {
        laserObj = new GameObject("Laser Beam");
        laser = laserObj.AddComponent<LineRenderer>();

        laser.material = material;
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.alignment = LineAlignment.View;
        laser.textureMode = LineTextureMode.Stretch;

        laserPoints = new List<Vector3>();
    }

    public void UpdateBeam(Vector3 startPos, Vector3 direction)
    {
        pos = startPos;
        dir = direction;
        laserPoints.Clear();
        CastRay(pos, dir);
    }

    private void CastRay(Vector3 pos, Vector3 dir)
    {
        laserPoints.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 300f, 1)) // Use proper layer mask if needed
        {
            if (hit.collider.CompareTag("Mirror"))
            {
                Vector3 reflectDir = Vector3.Reflect(dir, hit.normal);
                CastRay(hit.point, reflectDir);
            }
            else
            {
                laserPoints.Add(hit.point);
            }
        }
        else
        {
            laserPoints.Add(ray.GetPoint(300f));
        }

        UpdateLaser();
    }

    private void UpdateLaser()
    {
        laser.positionCount = laserPoints.Count;
        for (int i = 0; i < laserPoints.Count; i++)
        {
            laser.SetPosition(i, laserPoints[i]);
        }
    }

}

