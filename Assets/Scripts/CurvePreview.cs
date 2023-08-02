using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvePreview : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)]
    private float gizmosSphereSize;

    [SerializeField][Range(0.01f, 1f)]
    private float gizmosDensity = 0.01f;

    [SerializeField]
    private bool active;

    [SerializeField]
    private bool drawLines;

    [SerializeField]
    private Transform[] controlPoints;


    private void OnDrawGizmos()
    {
        if (!active) return;

        for (float t = 0; t < 1; t += gizmosDensity)
        {
            Vector3 gizmosPosition = GetPointOnBezierCurve(controlPoints.Length, t);
            Gizmos.DrawSphere(gizmosPosition, gizmosSphereSize);
        }

        if (!drawLines) return;

        for (int i = 0; i < controlPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(controlPoints[i].position, controlPoints[i+1].position);
        }
    }

    Vector3 GetPointOnBezierCurve(int numberOfPoints, float progress)
    {
        int n = numberOfPoints - 1;
        float t = progress;

        Vector3 point = new Vector3();
        for (int i = 0; i <= n; i++)
        {
            point += BinomCoefficient(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * controlPoints[i].position;
        }
        return point;
    }

    /// <summary>
    /// Calculates the binomial coefficient (nCk) (N items, choose k)
    /// </summary>
    /// <param name="n">the number items</param>
    /// <param name="k">the number to choose</param>
    /// <returns>the binomial coefficient</returns>
    public static long BinomCoefficient(long n, long k)
    {
        if (k > n) { return 0; }
        if (n == k) { return 1; } // only one way to chose when n == k
        if (k > n - k) { k = n - k; } // Everything is symmetric around n-k, so it is quicker to iterate over a smaller k than a larger one.
        long c = 1;
        for (long i = 1; i <= k; i++)
        {
            c *= n--;
            c /= i;
        }
        return c;
    }
}
