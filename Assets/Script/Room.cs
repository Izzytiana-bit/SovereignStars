using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Room
{
    public Direction[] exits;
    public GameObject prefab;
    private Direction entrance;


    public Room(RoomSO roomSO)
    {
        exits = roomSO.exits;
        prefab = roomSO.prefab;
    }

    public void SetEntrance(Direction direction)
    {
        entrance = direction;
    }

    public List<Direction> GetOpenExits()
    {
        List<Direction> openExits = new List<Direction>();
        foreach (Direction direction in exits)
        {
            if (direction != entrance)
                openExits.Add(direction);
        }
        return openExits;
    }
}

public enum Direction
{
    North,
    South,
    East,
    West
}