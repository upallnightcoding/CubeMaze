using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCntrl : MonoBehaviour
{
    [SerializeField] private GameObject borders;
    [SerializeField] private int id;

    public void ShowBorders(bool onOffSwitch) => borders.SetActive(onOffSwitch);

    public float GetHeight() => transform.position.y;

    public int GetId() => id;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
