using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell 
{
    // Type of Cell
    public MazeCellType type { get; private set; } = MazeCellType.UNVISITED;

    // References to north, south, east and west walls of a cell
    public MazeCell NorthWall { get; private set; } = null;
    public MazeCell SouthWall { get; private set; } = null;
    public MazeCell EastWall { get; private set; } = null;
    public MazeCell WestWall { get; private set; } = null;
    public MazeCell FrontWall { get; private set; } = null;
    public MazeCell BackWall { get; private set; } = null;

    // Cell Column and Row position
    public int Col { get; private set; }
    public int Row { get; private set; }
    public int Zee { get; private set; }

    public Vector3 Position { get; set; }

    public void MarkAsVisited() => type = MazeCellType.VISITED;

    public bool IsUnVisited() => (type == MazeCellType.UNVISITED);
    public bool IsVisited() => (type == MazeCellType.VISITED);

    public bool IsEqual(MazeCell target) =>
        ((target.Col == Col) && (target.Row == Row) && (target.Zee == Zee));

    // Predicate functions that returns true if a call exists
    public bool HasNorthWall() => NorthWall == null;
    public bool HasSouthWall() => SouthWall == null;
    public bool HasEastWall() => EastWall == null;
    public bool HasWestWall() => WestWall == null;
    public bool HasFrontWall() => FrontWall == null;
    public bool HasBackWall() => BackWall == null;

    public bool HasNorthPath() => NorthWall != null;
    public bool HasSouthPath() => SouthWall != null;
    public bool HasEastPath() => EastWall != null;
    public bool HasWestPath() => WestWall != null;
    public bool HasFrontPath() => FrontWall != null;
    public bool HasBackPath() => BackWall != null;

    public MazeCell(int col, int row, int zee)
    {
        this.Col = col;
        this.Row = row;
        this.Zee = zee;
    }

    public List<MazeCell> ListFreeNeighbor()
    {
        List<MazeCell> freeList = new List<MazeCell>();

        if (!HasNorthWall())    freeList.Add(NorthWall);
        if (!HasSouthWall())    freeList.Add(SouthWall);
        if (!HasEastWall())     freeList.Add(EastWall);
        if (!HasWestWall())     freeList.Add(WestWall);
        if (!HasFrontWall())    freeList.Add(FrontWall);
        if (!HasBackWall())     freeList.Add(BackWall);

        return (freeList);
    }

    public void CollapseWall(MazeCell neighbor)
    {
        int col = neighbor.Col - Col;
        int row = neighbor.Row - Row;
        int zee = neighbor.Zee - Zee;

        if (col == 1)
        {
            EastWall = neighbor;
            neighbor.WestWall = this;
        }

        if (col == -1)
        {
            WestWall = neighbor;
            neighbor.EastWall = this;
        }

        if (row == 1)
        {
            NorthWall = neighbor;
            neighbor.SouthWall = this;
        }

        if (row == -1)
        {
            SouthWall = neighbor;
            neighbor.NorthWall = this;
        }

        if (zee == 1)
        {
            BackWall = neighbor;
            neighbor.FrontWall = this;
        }

        if (zee == -1)
        {
            FrontWall = neighbor;
            neighbor.BackWall = this;
        }
    }
}

public enum MazeCellType
{
    UNVISITED,
    VISITED
}