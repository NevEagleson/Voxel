using UnityEngine;
using System.Collections;

namespace Voxel
{
    public enum Direction
    {
        North,
        East,
        South,
        West,
        Up,
        Down
    }

    public static class DirectionExtension
    {
        public static bool IsSide(this Direction thiz)
        {
            return thiz != Direction.Up && thiz != Direction.Down;
        }
        public static bool IsOpposite(this Direction thiz, Direction other)
        {
            switch (thiz)
            {
                case Direction.North:
                    return other == Direction.South;
                case Direction.East:
                    return other == Direction.West;
                case Direction.South:
                    return other == Direction.North;
                case Direction.West:
                    return other == Direction.East;
                case Direction.Up:
                    return other == Direction.Down;
                case Direction.Down:
                    return other == Direction.Up;
            }
            return false;
        }
    }
}

