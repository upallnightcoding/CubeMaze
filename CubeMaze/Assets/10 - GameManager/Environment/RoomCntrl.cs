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
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(axisX, 0.25f);
        transform.RotateAround(point, axisX, 0.1f);
    }
}
