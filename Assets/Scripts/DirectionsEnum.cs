using UnityEngine;
public enum Directions
{
    North = 0,
    East = 90,
    South = 180,
    West = 270
}

public static class DirectionsExtensions
{
    public static Directions RotateLeft(this Directions direction)
    {
        int newDir = ((int)direction - 90 + 360) % 360;
        return (Directions)newDir;
    }

    public static Directions RotateRight(this Directions direction)
    {
        int newDir = ((int)direction + 90) % 360;
        return (Directions)newDir;
    }

    public static Vector2Int ToVector2Int(this Directions direction)
    {
        switch (direction)
        {
            case Directions.North:
                return Vector2Int.up;
            case Directions.East:
                return Vector2Int.right;
            case Directions.South:
                return Vector2Int.down;
            case Directions.West:
                return Vector2Int.left;
            default:
                return Vector2Int.zero;
        }
    }
}
