using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianCntrl : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);    
    }
}
