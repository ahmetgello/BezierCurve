using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlle : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    [SerializeField]
    private float field;

    public bool clickingUpper;
    public bool clickBottom;

    void Update()
    {
        if(clickingUpper)
        {
            controlPoints[15].position += new Vector3(0, 0.01f * field, 0);
            controlPoints[16].position += new Vector3(0, 0.01f * field, 0);
            controlPoints[17].position += new Vector3(0, 0.01f * field, 0);
            controlPoints[18].position += new Vector3(0, 0.01f * field, 0);
        }
        else if(clickBottom)
        {
            controlPoints[15].position += new Vector3(0, -0.01f * field, 0);
            controlPoints[16].position += new Vector3(0, -0.01f * field, 0);
            controlPoints[17].position += new Vector3(0, -0.01f * field, 0);
            controlPoints[18].position += new Vector3(0, -0.01f * field, 0);
        }
        //controlPoints[15].position = new Vector3(controlPoints[15].position.x, Input.GetAxis("Vertical") * field, controlPoints[15].position.z);
        //controlPoints[16].position = new Vector3(controlPoints[16].position.x, Input.GetAxis("Vertical") * field, controlPoints[16].position.z);
        //controlPoints[17].position = new Vector3(controlPoints[17].position.x, Input.GetAxis("Vertical") * field, controlPoints[17].position.z);
        //controlPoints[18].position = new Vector3(controlPoints[18].position.x, Input.GetAxis("Vertical") * field, controlPoints[18].position.z);
    }

    public void UpperButtonClicked()
    {
        print("CLICK");
        clickingUpper = true;
    }

    public void UpperUnClick()
    {
        clickingUpper = false;
    }

    public void BottomButtonClicked()
    {
        clickBottom = true;
    }

    public void BottomUnClick()
    {
        clickBottom = false;
    }


}
