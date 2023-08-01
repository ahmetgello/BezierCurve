using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ad : MonoBehaviour
{

    [SerializeField]
    private float speed;

    void Update()
    {
        transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
    }
}
