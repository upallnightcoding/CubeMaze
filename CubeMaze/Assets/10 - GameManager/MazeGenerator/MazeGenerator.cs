using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator 
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Depth { get; private set; }

    private MazeCell[,,] maze = null;

    private bool MazeStackEmpty() => (mazeStack.Count == 0);

    private Stack<MazeCell> mazeStack;
    private List<MazeCell> mazeList;

    public MazeGenerator(int width, int height, int depth)
    {
        this.Width = width;
        this.Height = height;
        this.Depth = depth;

        mazeStack = new Stack<MazeCell>();
        maze = new MazeCell[Width, Height, Depth];
    }

    public void Generate()
    {
        InitMazeGenerator();

        while (!MazeStackEmpty())
        {
            WalkMaze(PickAValidNeighbor(mazeStack.Peek()));
        }

        //mazeList = new List<MazeCell>(maze.Values);
    }

    public MazeCell GetMazeCell(int col, int row, int zee)
    {
        return (!OffCube(col, row, zee) ? maze[col, row, zee] : null);
    }

    private bool OffCube(int x, int y, int z)
    {
        return (
            (x < 0) ||
            (y < 0) ||
            (z < 0) ||
            (x >= Width) ||
            (y >= Height) ||
            (z >= Depth));
    }

    public MazeCell PickRandomCell()
    {
        return (mazeList[UnityEngine.Random.Range(0, mazeList.Count)]);
    }

    private void WalkMaze(MazeCell neighbor)
    {
        if (neighbor != null)
        {
            neighbor.MarkAsVisited();
            mazeStack.Push(neighbor);
        }
        else
        {
            mazeStack.Pop();
        }
    }

    private MazeCell PickAValidNeighbor(MazeCell currentMazeCell)
    {
        Tuple<int, int, int>[] neighbors = {
            Tuple.Create( 0,  1,  0),   // North
            Tuple.Create( 0, -1,  0),   // South
            Tuple.Create( 1,  0,  0),   // East
            Tuple.Create(-1,  0,  0),   // West
            Tuple.Create( 0,  0, -1),   // Front
            Tuple.Create( 0,  0,  1)    // Back
        };

        List<MazeCell> validNeighborList = new List<MazeCell>();

        foreach (Tuple<int, int, int> neighbor in neighbors)
        {
            int col = currentMazeCell.Col + neighbor.Item1;
            int row = currentMazeCell.Row + neighbor.Item2;
            int zee = currentMazeCell.Zee + neighbor.Item3;

            MazeCell candidate = GetMazeCell(col, row, zee);

            if (candidate != null && (candidate.IsUnVisited()))
            {
                validNeighborList.Add(candidate);
            }
        }

        MazeCell validNeighbor = null;
        int nNeighbors = validNeighborList.Count;

        if (nNeighbors > 0)
        {
            validNeighbor = validNeighborList[GetRandom(nNeighbors)];
            currentMazeCell.CollapseWall(validNeighbor);
        }

        return (validNeighbor);
    }

    private void InitMazeGenerator()
    {
        BuildMaze();

        SetStartingCell();
    }

    private void BuildMaze()
    {
        for (int col = 0; col < Width; col++)
        {
            for (int row = 0; row < Height; row++)
            {
                for (int zee = 0; zee < Depth; zee++)
                {
                    maze[col, row, zee] = new MazeCell(col, row, zee);
                }
            }
        }
    }

    private void SetStartingCell()
    {
        MazeCell cell = GetMazeCell(0, 0, 0);

        if (cell != null)
        {
            cell.MarkAsVisited();
            mazeStack.Push(cell);
        }
    }

    /**
    GetRandom() - Returns a random number from 0 to n-1.
    */
    private int GetRandom(int n)
    {
        return (UnityEngine.Random.Range(0, n));
    }

    public void RenderMaze(float size)
    {
        for (int col = 0; col < Width; col++)
        {
            for (int row = 0; row < Height; row++)
            {
                for (int zee = 0; zee < Depth; zee++)
                {
                    MazeCell cell = maze[col, row, zee];

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = new Vector3(col*size, row*size, zee*size);
                    sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                    if (cell.HasNorthPath())
                    {
                        DrawPath(
                            size, 
                            new Vector3(col*size, row*size, zee*size), 
                            new Vector3(col*size, (row+1)*size, zee*size),
                            0.0f, 0.0f, 0.0f
                        );
                    }

                    if (cell.HasSouthPath())
                    {
                        DrawPath(
                            size,
                            new Vector3(col * size, row * size, zee * size),
                            new Vector3(col * size, (row-1) * size, zee * size),
                            0.0f, 0.0f, 0.0f
                        );
                    }

                    if (cell.HasEastPath())
                    {
                        DrawPath(
                            size,
                            new Vector3(col * size, row * size, zee * size),
                            new Vector3((col+1) * size, row * size, zee * size),
                            0.0f, 0.0f, 90.0f
                        );
                    }

                    if (cell.HasWestPath())
                    {
                        DrawPath(
                            size,
                            new Vector3(col * size, row * size, zee * size),
                            new Vector3((col-1) * size, row * size, zee * size),
                            0.0f, 0.0f, 90.0f
                        );
                    }

                    if (cell.HasFrontPath())
                    {
                        DrawPath(
                            size,
                            new Vector3(col * size, row * size, zee * size),
                            new Vector3(col * size, row * size, (zee-1) * size),
                            0.0f, 90.0f, 90.0f
                        );
                    }

                    if (cell.HasBackPath())
                    {
                        DrawPath(
                            size,
                            new Vector3(col * size, row * size, zee * size),
                            new Vector3(col * size, row * size, (zee+1) * size),
                            0.0f, 90.0f, 90.0f
                        );
                    }
                }
            }
        }
    }

    private void DrawPath(float size, Vector3 p1, Vector3 p2, float rx, float ry, float rz)
    {
        float x = (p1.x + p2.x) / 2.0f;
        float y = (p1.y + p2.y) / 2.0f;
        float z = (p1.z + p2.z) / 2.0f;

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        sphere.transform.position = new Vector3(x, y , z);
        sphere.transform.localScale = new Vector3(0.25f, 1, 0.25f);
        sphere.transform.localRotation = Quaternion.Euler(rx, ry, rz);
    }
}
