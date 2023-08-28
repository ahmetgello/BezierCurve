using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressingPanelManager : MonoBehaviour
{
    [SerializeField]
    private int roundCount;

    [SerializeField]
    private GameObject twoRoundGO, threeRoundGO;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject[] twoRoundsFlags;
    [SerializeField]
    private GameObject[] threeRoundsFlags;

    [SerializeField]
    private SpriteRenderer robotSprite;

    private float tParam;

    private void Start()
    {
        switch (roundCount)
        {
            case 2:
                //twoRoundGO.SetActive(true);
                //StartCoroutine(MoveRound1in2());
                break;
            case 3:
                //threeRoundGO.SetActive(true);
                //StartCoroutine(MoveRound3in3());
                break;
            default:
                break;
        }

    }

    public void button1click()
    {
        StartCoroutine(MoveRound1in2());
    }
    public void button2click()
    {
        StartCoroutine(MoveRound2in2());
    }
    public void button3click()
    {
        StartCoroutine(MoveRound1in3());
    }
    public void button4click()
    {
        StartCoroutine(MoveRound2in3());
    }
    public void button5click()
    {
        StartCoroutine(MoveRound3in3());
    }

    private IEnumerator MoveRound1in2()
    {
        twoRoundGO.SetActive(true);
        threeRoundGO.SetActive(false);

        robotSprite.enabled = true;
        CurvedSlider slider = twoRoundGO.GetComponent<CurvedSlider>();

        while(tParam < 0.45f)
        {
            tParam += Time.deltaTime * speed;

            slider.value = tParam;
            slider.DoSliderMagic(tParam);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        Phase1of2Done();
    }

    private void Phase1of2Done()
    {
        robotSprite.enabled = false;
        twoRoundsFlags[0].SetActive(true);
    }

    private void Phase2of2Done()
    {
        robotSprite.enabled = false;
        twoRoundsFlags[1].SetActive(true);
    }

    private void Phase1of3Done()
    {
        robotSprite.enabled = false;
        threeRoundsFlags[0].SetActive(true);
    }

    private void Phase2of3Done()
    {
        robotSprite.enabled = false;
        threeRoundsFlags[1].SetActive(true);
    }

    private void Phase3of3Done()
    {
        robotSprite.enabled = false;
        threeRoundsFlags[2].SetActive(true);
    }

    private IEnumerator MoveRound2in2()
    {
        twoRoundGO.SetActive(true);
        threeRoundGO.SetActive(false);


        // Activate previous flag
        twoRoundsFlags[0].SetActive(true);

        robotSprite.enabled = true;
        CurvedSlider slider = twoRoundGO.GetComponent<CurvedSlider>();

        tParam = .45f;
        while (tParam < 1f)
        {
            tParam += Time.deltaTime * speed;

            slider.value = tParam;
            slider.DoSliderMagic(tParam);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        Phase2of2Done();
    }

    private IEnumerator MoveRound1in3()
    {
        threeRoundGO.SetActive(true);
        twoRoundGO.SetActive(false);


        robotSprite.enabled = true;
        CurvedSlider slider = threeRoundGO.GetComponent<CurvedSlider>();

        speed = .2f;
        tParam = 0f;
        while (tParam < .26f)
        {
            tParam += Time.deltaTime * speed;

            slider.value = tParam;
            slider.DoSliderMagic(tParam);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        Phase1of3Done();
    }

    private IEnumerator MoveRound2in3()
    {
        threeRoundGO.SetActive(true);
        twoRoundGO.SetActive(false);

        threeRoundsFlags[0].SetActive(true);
        robotSprite.enabled = true;
        CurvedSlider slider = threeRoundGO.GetComponent<CurvedSlider>();

        speed = .2f;
        tParam = .26f;
        while (tParam < .62f)
        {
            tParam += Time.deltaTime * speed;

            slider.value = tParam;
            slider.DoSliderMagic(tParam);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        Phase2of3Done();
    }

    private IEnumerator MoveRound3in3()
    {
        threeRoundGO.SetActive(true);
        twoRoundGO.SetActive(false);

        threeRoundsFlags[0].SetActive(true);
        threeRoundsFlags[1].SetActive(true);
        robotSprite.enabled = true;
        CurvedSlider slider = threeRoundGO.GetComponent<CurvedSlider>();

        speed = .2f;
        tParam = .62f;
        while (tParam < 1f)
        {
            tParam += Time.deltaTime * speed;

            slider.value = tParam;
            slider.DoSliderMagic(tParam);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        Phase3of3Done();
    }
}
