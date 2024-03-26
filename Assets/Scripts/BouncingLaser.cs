using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingLaser : MonoBehaviour
{
    [SerializeField] private int maxBounce = 10;
    
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 dir = transform.right;
        Ray ray = new Ray(origin, dir);
        
        for (int i = 0; i < maxBounce; i++)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                    Gizmos.DrawLine(ray.origin, hit.point);

                    Vector3 calculatedReflection = CalculateReflectionVector(ray.direction, hit.normal);
                    Gizmos.DrawLine(hit.point, hit.point + calculatedReflection);

                    ray.direction = calculatedReflection;
                    ray.origin = hit.point;
            }
            else
            {
                break;
            }
        }
    }

    private Vector3 CalculateReflectionVector(Vector3 incomingDir, Vector3 normal)
    {
        float scalarProjection = Vector3.Dot(incomingDir, normal);
        return incomingDir - 2 * scalarProjection * normal;
    }
}
