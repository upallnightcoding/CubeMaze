using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotatePlatform(Vector3 point, Vector3 axis)
    {
        StartCoroutine(RotateObject(point, axis, 90.0f, gameData.cubeRotationSpeed));
    }

    private IEnumerator RotateObject(Vector3 point, Vector3 axis, float rotateAmount, float rotateTime)
    {
        if (!turning)
        {
            turning = true;

            float step = 0.0f;
            float rate = 1.0f / rotateTime;
            float smoothStep = 0.0f;
            float lastStep = 0.0f;

            while (step < 1.0)
            {
                step += (float)(Time.deltaTime * rate);
                smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);
                transform.RotateAround(point, axis, (float)(rotateAmount * (smoothStep - lastStep)));
                lastStep = smoothStep;

                yield return null;
            }

            if (step > 1.0) transform.RotateAround(point, axis, (float)(rotateAmount * (1.0 - lastStep)));

            turning = false;
        }
    }
}
