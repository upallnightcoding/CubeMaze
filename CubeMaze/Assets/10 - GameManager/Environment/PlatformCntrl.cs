using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCntrl : MonoBehaviour
{
    [SerializeField] private GameObject[] borderCntrls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoarders(bool value)
    {
        foreach(GameObject border in borderCntrls)
        {
            border.SetActive(value);
        }
    }
}
