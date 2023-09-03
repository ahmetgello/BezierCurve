using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingTheGame : MonoBehaviour
{

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject player;

    public void PlayClicked()
    {
        anim.Play("StartButton");
        StartCoroutine(removePanel());
    }

    IEnumerator removePanel()
    {
        yield return new WaitForSeconds(0.3f);
        player.SetActive(true);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
