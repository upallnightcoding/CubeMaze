using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private PlatformCntrl[] platformCntrlList;

    private bool turning = false;

    private int activePlatform = 0;

    private Dictionary<int, int[]> platformMap = new Dictionary<int, int[]>();

    // Start is called before the first frame update
    void Start()
    {
        // Index => North, East, South, West
        //----------------------------------
        platformMap.Add(0, new int[] {1, 4, 3, 5});
        platformMap.Add(1, new int[] {2, 4, 0, 5});
        platformMap.Add(2, new int[] {3, 4, 1, 5});
        platformMap.Add(3, new int[] {0, 4, 2, 5});
        platformMap.Add(4, new int[] {0, 1, 2, 3});
        platformMap.Add(5, new int[] {1, 0, 3, 2});

        foreach(PlatformCntrl control in platformCntrlList)
        {
            if(control)
            {
                control.ShowBorders(false);
            }
        }

        platformCntrlList[activePlatform].ShowBorders(true);
    }

    public void RotatePlatform(Vector3 point, Vector3 axis, string direction)
    {
        StartCoroutine(RotateObject(point, axis, 90.0f, gameData.cubeRotationSpeed, direction));
    }

    private int CalcNextPlatform(string direction)
    {
        int platform = -1;

        platformMap.TryGetValue(activePlatform, out int[] moveTo);

        switch(direction)
        {
            case "North":
                platform = moveTo[0];
                break;
            case "South":
                platform = moveTo[2];
                break;
            case "East":
                platform = moveTo[1];
                break;
            case "West":
                platform = moveTo[3];
                break;
        }

        return (platform);
    }

    private int FindBottomPlatform()
    {
        int found = -1;
        float shortestDistance = 99999.0f;

        foreach (PlatformCntrl platform in platformCntrlList)
        {
            if (platform) { 
                float height = platform.GetHeight();

                if (height < shortestDistance)
                {
                    found = platform.GetId();
                    shortestDistance = height;
                }
            }
        }

        return (found);
    }

    private IEnumerator RotateObject(Vector3 point, Vector3 axis, float rotateAmount, float rotateTime, string direction)
    {
        if (!turning)
        {
            turning = true;

            float step = 0.0f;
            float rate = 1.0f / rotateTime;
            float smoothStep = 0.0f;
            float lastStep = 0.0f;

            platformCntrlList[activePlatform].ShowBorders(false);

            while (step < 1.0)
            {
                step += (float)(Time.deltaTime * rate);
                smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);
                transform.RotateAround(point, axis, (float)(rotateAmount * (smoothStep - lastStep)));
                lastStep = smoothStep;

                yield return null;
            }

            if (step > 1.0) transform.RotateAround(point, axis, (float)(rotateAmount * (1.0 - lastStep)));

            yield return new WaitForSeconds(0.1f);

            Debug.Log($"Current Platform: {activePlatform}");

            //activePlatform = CalcNextPlatform(direction);
            activePlatform = FindBottomPlatform();

            Debug.Log($"New Platform: {activePlatform}");

            platformCntrlList[activePlatform].ShowBorders(true);

            turning = false;
        }
    }
}
