using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ad;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private float interval;

    private void Start()
    {
        StartCoroutine(SpawnAds());
    }

    IEnumerator SpawnAds()
    {
        yield return new WaitForSeconds(interval);
        int spawnPlace = Random.Range(0, spawnPoints.Length);

        Instantiate(ad, spawnPoints[spawnPlace].position, Quaternion.identity);
        StartCoroutine(SpawnAds());
    }
}
