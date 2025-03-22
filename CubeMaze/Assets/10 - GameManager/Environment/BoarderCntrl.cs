using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderCntrl : MonoBehaviour
{
    [SerializeField] private CubeCntrl cubeCntrl;
    [SerializeField] private Transform pivotPoint;

    private Vector3 posAxisX = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 negAxisX = new Vector3(-1.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Boarder OnTriggerEnter ... {transform.tag}");

        switch(transform.tag)
        {
            case "North":
                cubeCntrl.RotatePlatform(pivotPoint.position, posAxisX);
                break;
            case "South":
                cubeCntrl.RotatePlatform(pivotPoint.position, negAxisX);
                break;
        }

        
    }
}
