using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private float speed = 5.0f;
    private float rotatey = 0.0f;
    private float rotateSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
        }

        if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(-transform.forward * speed * Time.deltaTime);
        }

        if (Keyboard.current.qKey.isPressed)
        {
            rotatey += rotateSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0.0f, rotatey, 0.0f);
        }

        if (Keyboard.current.eKey.isPressed)
        {
            rotatey -= rotateSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0.0f, rotatey, 0.0f);
        }
    }
}
