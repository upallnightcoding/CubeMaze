using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCntrl : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;

    [SerializeField] private Transform portalCamera;
    [SerializeField] private Transform portalCameraPos;

    private Vector3 player = new Vector3();
    private Vector3 portal = new Vector3();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.x = playerTrans.position.x;
        player.y = 0.0f;
        player.z = playerTrans.position.z;

        portal.x = transform.position.x;
        portal.y = 0.0f;
        portal.z = transform.position.z;

        Vector3 direction = (portal - playerTrans.position).normalized;
        float distance = Vector3.Distance(portal, playerTrans.position);
    }
}
