using System.Collections.Generic;
using UnityEngine;

public class LaserBeam 
{
    private Vector3 pos, dir;
    private GameObject laserObj;
    private LineRenderer laser;
    private List<Vector3> laserIndicies;

    public LaserBeam(Material material)
    {
        laserObj = new GameObject("Laser Beam");
        laser = laserObj.AddComponent<LineRenderer>();
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.material = material;
        laser.startColor = material.color;
        laser.endColor = material.color;

        laserIndicies = new List<Vector3>();
    }

    public void UpdateBeam(Vector3 startPos, Vector3 direction)
    {
        pos = startPos;
        dir = direction;
        laserIndicies.Clear();

        CastRay(pos, dir);
    }

    private void CastRay(Vector3 pos, Vector3 dir)
    {
        laserIndicies.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30f, 1)) // Layer mask = 1
        {
            CheckHit(hit, dir);
        }
        else
        {
            laserIndicies.Add(ray.GetPoint(30f));
            UpdateLaser();
        }
    }

    private void CheckHit(RaycastHit hitInfo, Vector3 direction)
    {
        if (hitInfo.collider.CompareTag("Mirror"))
        {
            Vector3 reflectedDir = Vector3.Reflect(direction, hitInfo.normal);
            Vector3 hitPoint = hitInfo.point;

            CastRay(hitPoint, reflectedDir);
        }
        else
        {
            laserIndicies.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    private void UpdateLaser()
    {
        laser.positionCount = laserIndicies.Count;
        for (int i = 0; i < laserIndicies.Count; i++)
        {
            laser.SetPosition(i, laserIndicies[i]);
        }
    }

}

