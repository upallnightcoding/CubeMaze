using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCntrl : MonoBehaviour
{
    [SerializeField] private CubeCntrl cubeCntrl;
    [SerializeField] private Transform pivotPoint;

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
        Debug.Log($"On Trigger Enter ... {transform.tag}");

        switch(transform.tag)
        {
            case "North":
                cubeCntrl.RotatePlatform(pivotPoint.position, transform.right);
                break;
            case "South":
                cubeCntrl.RotatePlatform(pivotPoint.position, transform.right);
                break;
        }
    }
}
