using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;

public class RoomRequirements
{
    public List<Direction> exits = new List<Direction>();

    public bool IsValid(RoomSO room)
    {
        foreach(Direction direction in exits)
        {
            if (!room.exits.Contains(direction))
            {
                return false;
            }
        }
        return true;
    }

}
