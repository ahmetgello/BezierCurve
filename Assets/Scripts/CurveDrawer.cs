using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveDrawer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private Vector3[] linePoints;

    [SerializeField]
    [Range(0, 100)]
    private int quality;

    [SerializeField]
    [Range(0f, 1f)]
    private float value;

    [SerializeField]
    private bool drawInEditMode;

    [SerializeField]
    private Transform[] controlPoints;

    private void Start()
    {
        DrawLine(value);
    }

    private void OnDrawGizmos()
    {
        if (!drawInEditMode) return;
        DrawLine(value);
    }

    void DrawLine(float completeness = 1f)
    {
        int q = (int)(completeness * quality);

        linePoints = new Vector3[q];
        lineRenderer.positionCount = q;

        for (int i = 0; i < q; i++)
        {
            float t = i / (float)quality;
            linePoints[i] = GetPointOnBezierCurve(controlPoints.Length, t);
        }
        lineRenderer.SetPositions(linePoints);
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
