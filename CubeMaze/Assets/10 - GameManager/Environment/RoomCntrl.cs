using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCntrl : MonoBehaviour
{
    private Vector3 axisX = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 point = new Vector3(0.0f, 0.0f, 10.0f);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateObject(point, axisX, 90.0f, 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(axisX, 0.25f);
        //transform.RotateAround(point, axisX, 0.1f);
    }

    private IEnumerator RotateObject(Vector3 point, Vector3 axis, float rotateAmount, float rotateTime )
    {
        float step = 0.0f; //non-smoothed
        var rate = 1.0 / rotateTime; //amount to increase non-smooth step by
        var smoothStep = 0.0; //smooth step this time
        var lastStep = 0.0; //smooth step last time

        while (step < 1.0)
        { 
            step += (float)(Time.deltaTime * rate); 
            smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step); 
            transform.RotateAround(point, axis, (float)(rotateAmount * (smoothStep - lastStep)));
            lastStep = smoothStep; 

            yield return null; 
        }
        
        if (step > 1.0) transform.RotateAround(point, axis, (float) (rotateAmount * (1.0 - lastStep)));
    }
}
