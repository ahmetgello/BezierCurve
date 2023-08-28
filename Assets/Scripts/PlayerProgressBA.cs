using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressBA : MonoBehaviour
{
    [SerializeField]
    private GameObject startingSprite;

    [SerializeField]
    private GameObject progressingSprite;

    [SerializeField]
    private GameObject sectionCicle;

    [SerializeField]
    private GameObject flag;
    private LineRenderer line;

    [SerializeField]
    private LineRenderer progressedLine;

    [SerializeField]
    private Transform[] lineControlPoints;

    [SerializeField]
    [Range(1f, 100f)]
    private float lineQuality;

    [SerializeField]
    [Range(0f, 1f)]
    private float lineCompleteness;

    private Vector3[] linePoints;

    [SerializeField]
    private int numberOfRounds;

    [SerializeField]
    private Sprite[] roundSprites;

    [SerializeField]
    private SpriteRenderer roundSpriteRenderer;

    void Start()
    {
        MakeFirstSprite();
        AddDottedLine(numberOfRounds);
        DrawProgressLine(lineCompleteness);
        StartCoroutine(LineProgress(lineCompleteness));
    }

    IEnumerator LineProgress(float value)
    {
        for (float t = 0; t < 1; t += 0.0001f)
        {
            DrawProgressLine(t);
            progressingSprite.transform.position = Vector3.Lerp(lineControlPoints[0].position, lineControlPoints[lineControlPoints.Length - 1].position, t);
            yield return new WaitForEndOfFrame();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(lineControlPoints[0].position, 0.8f);
        for (float t = 0; t < 1; t += 0.02f)
        {
            Vector3 gizmosPosition = GetPointOnBezierCurve(lineControlPoints.Length, t);
            Gizmos.DrawWireSphere(gizmosPosition, 0.2f);
        }
    }

    void MakeFirstSprite()
    {
        Instantiate(startingSprite, lineControlPoints[0].position, Quaternion.identity, transform);
    }

    void DrawProgressLine(float completeness = 1f)
    {
        int q = (int)(completeness * lineQuality);

        linePoints = new Vector3[q];
        progressedLine.positionCount = q;

        for (int i = 0; i < q; i++)
        {
            float t = i / (float)lineQuality;
            linePoints[i] = GetPointOnBezierCurve(lineControlPoints.Length, t);
        }
        progressedLine.SetPositions(linePoints);
    }

    private void AddDottedLine(int rounds)
    {
        switch (rounds)
        {
            case 2:
                roundSpriteRenderer.sprite = roundSprites[0];
                break;
            case 3:
                roundSpriteRenderer.sprite = roundSprites[1];
                break;
            default:
                break;
        }
    }



    #region helper functions

    Quaternion RotateToDirection(Vector3 current, Vector3 target, float addToRotaion)
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
            point += BinomCoefficient(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * lineControlPoints[i].position;
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
