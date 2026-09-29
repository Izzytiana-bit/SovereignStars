using System.Linq;
using System.Linq.Expressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Dungeon : MonoBehaviour
{
    public int width;
    public int height;
    public RoomSO startingRoom;
    public RoomSO[] endRooms;
    public int minExitDistance;
    private int startingColumn;
    private int startingRow;
    public Room[,] grid;
    public int RoomsToGenerate;
    public int minRooms;
    public RoomSO[] roomPrefabs;
    private int currentTotalRooms;

    public float roomWidth;
    public float roomHeight;
    public float roomVerticalOffset;

    private int endColumn;
    private int endRow;

    private DungeonRequirements requirements;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(width * height < minRooms)
        {
            Debug.LogError("This dungeon is ass (minimum rooms too high)");
            return;
        }

        while (currentTotalRooms < minRooms)
        {
            GenerateDungeon();
        }
        SpawnRooms();
    }

    void GenerateDungeon()
    {
        currentTotalRooms = 0;
        grid = new Room[width, height];

        GenerateStartingRoom();

        ChooseEndPosition();

        requirements = new DungeonRequirements(this, startingColumn, startingRow, endColumn, endRow);
        requirements.FindRequiredPath();

        GenerateEndRoom();



        foreach (Direction targetDir in grid[startingColumn, startingRow].GetOpenExits())
        {
            switch (targetDir)
            {
                case Direction.North:
                    GenerateRoom(startingColumn, startingRow + 1, Direction.South);
                    break;
                case Direction.South:
                    GenerateRoom(startingColumn, startingRow - 1, Direction.North);
                    break;
                case Direction.West:
                    GenerateRoom(startingColumn - 1, startingRow, Direction.East);
                    break;
                case Direction.East:
                    GenerateRoom(startingColumn + 1, startingRow, Direction.West);
                    break;
            }
        }
    }

    void GenerateStartingRoom()
    {
        startingRow = 0;
        startingColumn = Random.Range(0, width);
        grid[startingColumn, startingRow] = new Room(startingRoom);
        grid[startingColumn, startingRow].SetEntrance(Direction.South);
        currentTotalRooms++;
    }

    void ChooseEndPosition()
    {
        endRow = startingRow;
        endColumn = startingColumn;
        while (Distance(startingColumn, startingRow, endColumn, endRow) < minExitDistance)
        {
            endColumn = Random.Range(0, width);
            endRow = Random.Range(0, height);
        }
    }

    void GenerateEndRoom()
    {
        RoomSO toSpawn = SelectRoom(endColumn, endRow, endRooms);
        grid[endColumn, endRow] = new Room(toSpawn);
        currentTotalRooms++;
    }

    int Distance(int startingColumn, int startingRow, int endColumn, int endRow)
    {
        return Mathf.Abs(endColumn - startingColumn) + Mathf.Abs(endRow - startingRow);
    }

    void SpawnRooms()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null)
                {
                    GameObject spawned = Instantiate(grid[x, y].prefab, transform);
                    spawned.transform.position = GetRoomPosition(x, y);
                }
            }
        }
    }

    void GenerateRoom(int column, int row, Direction entrance)
    {
        if (currentTotalRooms >= RoomsToGenerate)
            return;
        if (column < 0 || column >= width)
            return;
        if (row < 0 || row >= height)
            return;
        if (grid[column, row] != null)
            return;
        RoomSO toSpawn = SelectRoom(column, row, roomPrefabs);
        currentTotalRooms++;
        grid[column, row] = new Room(toSpawn);
        grid[column, row].SetEntrance(entrance);
        foreach (Direction targetDir in grid[column, row].GetOpenExits())
        {
            switch (targetDir)
            {
                case Direction.North:
                    GenerateRoom(column, row + 1, Direction.South);
                    break;
                case Direction.South:
                    GenerateRoom(column, row - 1, Direction.North);
                    break;
                case Direction.West:
                    GenerateRoom(column - 1, row, Direction.East);
                    break;
                case Direction.East:
                    GenerateRoom(column + 1, row, Direction.West);
                    break;
            }
        }
    }

    RoomSO SelectRoom(int column, int row, RoomSO[] rooms)
    {
        bool found = false;

        RoomSO room = rooms[Random.Range(0, rooms.Length)];
        int attempts = 0;
        while (!found && attempts < 1000)
        {
            attempts++;
            if (DoRoomsMatch(room, column, row, Direction.North) &&
                DoRoomsMatch(room, column, row, Direction.South) &&
                DoRoomsMatch(room, column, row, Direction.East) &&
                DoRoomsMatch(room, column, row, Direction.West) && 
                (requirements.grid[column, row] == null || requirements.grid[column, row].IsValid(room)))
            {

                found = true;
            }
            else
            {
                room = rooms[Random.Range(0, rooms.Length)];
            }
        }
        if(attempts >= 1000)
        {
            Debug.LogError("Failed to find valid room for :" + column + ", " + row);
        }

        return room;
    }

    Vector3 GetRoomPosition(int column, int row)
    {
        return new Vector3(column * roomWidth, roomVerticalOffset, row * roomHeight);
    }

    bool DoRoomsMatch(RoomSO current, int column, int row, Direction direction)
    {
        int targetRow = GetRowInDirection(row, direction);
        int targetColumn = GetColumnInDirection(column, direction);


        // check if column is outside of the grid
        if (targetColumn < 0 || targetColumn >= width)
        {
            if (current.exits.Contains(direction))
                return false;
            else
                return true;
        }

        // check if row is outside the grid
        if (targetRow < 0 || targetRow >= height)
        {
            if (current.exits.Contains(direction))
                return false;
            else
                return true;
        }

        Room target = grid[targetColumn, targetRow];

        if (target == null)
        {
            return true;
        }

        if (current.exits.Contains(direction) &&
            target.exits.Contains(GetOppositeDirection(direction)))
        {
            return true;
        }

        if (!current.exits.Contains(direction) &&
            !target.exits.Contains(GetOppositeDirection(direction)))
        {
            return true;
        }

        return false;
    }

    public static Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.West:
                return Direction.East;
            default: // case Direction.East:
                return Direction.West;

        }
    }

    public static int GetRowInDirection(int row, Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return row + 1;
            case Direction.South:
                return row - 1;
            default:
                return row;

        }
    }

    public static int GetColumnInDirection(int column, Direction direction)
    {
        switch (direction)
        {
            case Direction.East:
                return column + 1;
            case Direction.West:
                return column - 1;
            default:
                return column;

        }
    }
}