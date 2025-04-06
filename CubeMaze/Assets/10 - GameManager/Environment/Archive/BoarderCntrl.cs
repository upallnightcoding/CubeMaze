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

    
}
