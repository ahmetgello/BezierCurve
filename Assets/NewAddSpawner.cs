using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAddSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ad;

    void Start()
    {
        int diff = PlayerPrefs.GetInt("Diff", 1);

        switch (diff)
        {
            case 1:
                Instantiate(ad, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(ad, new Vector3(0, -1, 0), Quaternion.identity);
                Instantiate(ad, new Vector3(0, 1, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(ad, new Vector3(0, -2, 0), Quaternion.identity);
                Instantiate(ad, new Vector3(0, 0, 0), Quaternion.identity);
                Instantiate(ad, new Vector3(0, 2, 0), Quaternion.identity);
                break;
            default:
                Instantiate(ad, new Vector3(0, -2, 0), Quaternion.identity);
                Instantiate(ad, new Vector3(0, 0, 0), Quaternion.identity);
                Instantiate(ad, new Vector3(0, 2, 0), Quaternion.identity);
                break;

        }
    }
}
