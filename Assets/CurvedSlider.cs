using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedSlider : MonoBehaviour
{
    [Range(0f, 1f)]
    public float value;

    [SerializeField]
    private GameObject handle;

    [SerializeField]
    private Transform[] controlPoints;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private float lineQuality;

    [SerializeField]
    [Range(0f, 360f)]
    private float handleRotationModifier;

    [SerializeField]
    private bool preview;


    //private void Update()
    //{
    //    DoSliderMagic(value);
    //}

    // Preview
    private void OnDrawGizmos()
    {
        if (!preview) return;


        for (float t = 0; t < 1; t += 0.01f)
        {
            Vector3 gizmosPos = GetPointOnBezierCurve(controlPoints.Length, t);
            Gizmos.DrawSphere(gizmosPos, 0.2f);
        }
    }


    public void DoSliderMagic(float completeness = 1f)
    {
        int q = (int)(completeness * lineQuality);

        if (q < 2) return;

        Vector3[] linePoints = new Vector3[q];
        lineRenderer.positionCount = q;

        for (int i = 0; i < q; i++)
        {
            float t = i / (float)lineQuality;
            linePoints[i] = GetPointOnBezierCurve(controlPoints.Length, t);
        }
        lineRenderer.SetPositions(linePoints);
        handle.transform.position = linePoints[linePoints.Length - 1];

        handle.transform.rotation = RotateToDirection(linePoints[linePoints.Length - 2], linePoints[linePoints.Length - 1], handleRotationModifier);
        
    }

    #region helper functions

    Quaternion RotateToDirection(Vector3 current, Vector3 target, float addToRotaion = 0)
    {
        Vector3 vectorToTarget = current - target;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + addToRotaion;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        return q;
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
    private long BinomCoefficient(long n, long k)
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

    #endregion
}
