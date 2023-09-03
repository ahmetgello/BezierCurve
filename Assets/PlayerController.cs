using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float xSpeed, ySpeed;

    private LineRenderer lineRenderer;

    float hori;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //StartCoroutine(AddPoint());
    }

    private void Update()
    {
        //hori = Input.GetAxisRaw("Horizontal");
        if (hori == 1) hori = 0;
        transform.position += new Vector3(0, Time.deltaTime * ySpeed, 0);
        transform.position = new Vector3(hori, transform.position.y, transform.position.z);
    }

    private IEnumerator AddPoint()
    {
        yield return new WaitForSeconds(0.05f);
        AddCurrentPoint();
        StartCoroutine(AddPoint());
    }

    private void AddCurrentPoint()
    {
        lineRenderer.positionCount = lineRenderer.positionCount + 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
    }

    private void FixedUpdate()
    {
        lineRenderer.positionCount = lineRenderer.positionCount + 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ad"))
        {
            Lose();
        }
        else if (collision.CompareTag("End"))
        {
            Win();
        }
    }

    private void Win()
    {
        PlayerPrefs.SetInt("Diff", PlayerPrefs.GetInt("Diff", 1) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ClickingScreen()
    {
        hori = -1;
    }

    public void UnClickedScreen()
    {
        hori = 0;
    }
}
