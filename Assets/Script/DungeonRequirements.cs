using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LightTransport.PostProcessing;

public class DungeonRequirements
{

    int width;
    int height;
    int endColumn;
    int endRow;
    int startColumn;
    int startRow;

    Direction startingDirection;

    public RoomRequirements[,] grid;

    public DungeonRequirements(Dungeon dungeon, int startColumn, int startRow, int endColumn, int endRow)
    {
        width = dungeon.width;
        height = dungeon.height;
        this.endColumn = endColumn;
        this.endRow = endRow;
        this.startColumn = startColumn;
        this.startRow = startRow;

        startingDirection = dungeon.grid[startColumn, startRow].exits[0];

        grid = new RoomRequirements[width, height];
    }

    public void FindRequiredPath()
    {
        int walkColumn = Dungeon.GetColumnInDirection(startColumn, startingDirection);
        int walkRow = Dungeon.GetRowInDirection(startRow,startingDirection);
        Direction walkEntrance = Dungeon.GetOppositeDirection(startingDirection);

        List<(int, int)> path = new List<(int, int)>();
        bool pathFound = false;
        while (!pathFound)
        {
            path = new List<(int, int)>();
            pathFound = DrunkenWalk(walkColumn, walkRow, walkEntrance, path);
        }

        for(int i = 0; i < path.Count; i++)
        {
            RoomRequirements currentRoom = new RoomRequirements();
            grid[path[i].Item1, path[i].Item2] = currentRoom;
            if(i == 0)
            {
                currentRoom.exits.Add(walkEntrance);
            }
            else
            {
                currentRoom.exits.Add(GetDirection(path[i].Item1, path[i].Item2, path[i - 1].Item1, path[i - 1].Item2));
            }

            if (i != path.Count - 1)
            {
                currentRoom.exits.Add(GetDirection(path[i].Item1, path[i].Item2, path[i + 1].Item1, path[i + 1].Item2));
            }
        }
    }

    Direction GetDirection(int fromColumn, int fromRow, int toColumn, int toRow)
    {
        if(toColumn > fromColumn)
        {
            return Direction.East;
        }
        if(toColumn < fromColumn)
        {
            return Direction.West;
        }
        if(toRow > fromRow)
        {
            return Direction.North;
        }
        return Direction.South;
    }

    bool DrunkenWalk(int column, int row, Direction entrance, List<(int,int)> path)
    {
        path.Add((column, row));
        if (column == endColumn && row == endRow)
        {
            return true;
        }
        if(!DoesValidStepExist(column, row, path))
        {
            return false;
        }
        Direction stepDirection = GetRandomDirection();
        while(!IsStepValid(column,row, path, stepDirection))
        {
            stepDirection = GetRandomDirection();
        }

        int stepColumn = Dungeon.GetColumnInDirection(column, stepDirection);
        int stepRow = Dungeon.GetRowInDirection(row, stepDirection);

        return DrunkenWalk(stepColumn, stepRow,stepDirection,path);

    }

    bool DoesValidStepExist(int column, int row, List<(int, int)> path)
    {
        if(IsStepValid(column, row, path, Direction.North))
        {
            return true;
        }
        if(IsStepValid(column, row, path, Direction.South))
        {
            return true;
        }
        if (IsStepValid(column, row, path, Direction.East))
        {
            return true;
        }
        if (IsStepValid(column, row, path, Direction.West))
        {
            return true;
        }

        return false;
    }

    bool IsStepValid(int column, int row, List<(int, int)> path, Direction direction)
    {
        int stepColumn = Dungeon.GetColumnInDirection(column, direction);
        int stepRow = Dungeon.GetRowInDirection(row, direction);

        if(stepColumn < 0 || stepColumn >= width)
        {
            return false;
        }
        if(stepRow < 0 || stepRow >= height)
        {
            return false;
        }
        if(stepRow == startRow && stepColumn == startColumn)
        {
            return false;
        }
        foreach((int, int) position in path)
        {
            if(position.Item1 == stepColumn && position.Item2 == stepRow)
            {
                return false;
            }
        }

        return true;
    }

    Direction GetRandomDirection()
    {
        int choice = Random.Range(0, 4);
        switch (choice)
        {
            case 0: 
                return Direction.North;
            case 1: 
                return Direction.South;
            case 2: 
                return Direction.East;
            default: 
                return Direction.West;
        }
    }

}
