using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private LineRenderer lineRenderer;

    private Vector3[] pointsOnTriangle;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(points[0].position, points[1].position);
        Gizmos.DrawLine(points[1].position, points[2].position);
        Gizmos.DrawLine(points[0].position, points[2].position);

        //pointsOnTriangle = new Vector3[75];
        //lineRenderer.positionCount = 75;

        //for (int t = 0; t < 25; t += 1)
        //{
        //    pointsOnTriangle[t] = Vector3.Lerp(points[0].position, points[1].position, t / 25f);
        //}

        //for (int t = 25; t < 50; t += 1)
        //{
        //    pointsOnTriangle[t] = Vector3.Lerp(points[1].position, points[2].position, t / 25f);
        //}

        //for (int t = 50; t < 75; t += 1)
        //{
        //    pointsOnTriangle[t] = Vector3.Lerp(points[0].position, points[2].position, t / 25f);
        //}

        lineRenderer.SetPosition(0, points[0].position);
        lineRenderer.SetPosition(1, points[1].position);
        lineRenderer.SetPosition(2, points[2].position);
        //lineRenderer.SetPositions(pointsOnTriangle);

    }
}
