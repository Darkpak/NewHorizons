using System.Collections.Generic;
using UnityEngine;

public class LaserBeam 
{
    Vector3 pos, dir;
    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndicies = new List<Vector3>();
    public LaserBeam(Vector3 pos,Vector3 dir, Material material)
    {
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent<LineRenderer>();
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        laser.material = material;

        //Color laserColor = new Color(1f, 1f, 0f, 0.3f); // yellow with 30% opacity
        laser.startColor = material.color;
        laser.endColor = material.color;
        //laser.startColor.a = material.color.a;

        CastRay(pos,dir, laser);
    }

    public void CastRay(Vector3 pos,Vector3 dir,LineRenderer laser)
    {
        laserIndicies.Add(pos);
        Ray ray = new Ray(pos,dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit,dir,laser);
        }
        else
        {
            laserIndicies.Add(ray.GetPoint(30));
            UpdateLaser();
        }

    }

    public void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndicies.Count;
        foreach (Vector3 item in laserIndicies)
        {
            laser.SetPosition(count, item);
            count++;
        }

    }
    public void CheckHit(RaycastHit hitInfo,Vector3 direction,LineRenderer laser)
    {
        if(hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, laser);
        }
        else
        {
            laserIndicies.Add(hitInfo.point);
            UpdateLaser();
        }

    }
   
}

