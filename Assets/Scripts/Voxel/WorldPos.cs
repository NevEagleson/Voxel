using UnityEngine;

namespace Voxel
{
    public struct WorldPos
    {
        public int X, Y, Z;

        public WorldPos(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator WorldPos(Vector3 vec)
        {
            WorldPos result = new WorldPos();
            result.X = Mathf.RoundToInt(vec.x);
            result.Y = Mathf.RoundToInt(vec.y);
            result.Z = Mathf.RoundToInt(vec.z);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WorldPos)) return false;
            WorldPos other = (WorldPos)obj;
            return !(X != other.X || Y != other.Y || Z != other.Z);
        }
        public override int GetHashCode()
        {
            //naive but fast - just spread coords across bit pattern
            return X + Y << 11 + Z << 22;
        }

        public static WorldPos operator +(WorldPos a, WorldPos b)
        {
            return new WorldPos(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static WorldPos operator -(WorldPos a, WorldPos b)
        {
            return new WorldPos(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
    }
}

