using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCntrl : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private int xSize, ySize, zSize;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator maze = new(xSize, ySize, zSize);

        maze.Generate();

        maze.RenderMaze(size);
    }

   
}
