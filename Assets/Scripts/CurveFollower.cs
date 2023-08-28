using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveFollower : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform[] controlPoints;

    [SerializeField][Range(0f, 1f)]
    private float start, end;

    private void Start()
    {
        StartCoroutine(FollowCurve(start, end));
    }

    IEnumerator FollowCurve(float start, float end)
    {
        for (float t = start; t < end; t += Time.deltaTime * speed)
        {
            Vector3 nextPos = GetPointOnBezierCurve(controlPoints.Length, t);
            transform.position = nextPos;
            yield return new WaitForEndOfFrame();
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
}
