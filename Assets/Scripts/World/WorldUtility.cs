using UnityEngine;
using System.Collections;

namespace Voxel
{
    public static class WorldUtility
    {
		public static int PositionToChunk(int x)
		{
			return Mathf.FloorToInt(x / (float)Constants.ChunkSize) * Constants.ChunkSize;
		}
		public static int PositionToChunk(float x)
		{
			return Mathf.FloorToInt(x / (float)Constants.ChunkSize) * Constants.ChunkSize;
		}

		public static WorldPos GetBlockPos(RaycastHit hit, bool adjacent = false)
        {
            return new Vector3(
                MoveWithinBlock(hit.point.x, hit.normal.x, adjacent),
                MoveWithinBlock(hit.point.y, hit.normal.y, adjacent),
                MoveWithinBlock(hit.point.z, hit.normal.z, adjacent)
                );
        }

        static float MoveWithinBlock(float pos, float norm, bool adjacent = false)
        {
            if (pos - (int)pos == 0.5f || pos - (int)pos == -0.5f)
            {
                if (adjacent)
                {
                    pos += (norm / 2);
                }
                else
                {
                    pos -= (norm / 2);
                }
            }

            return (float)pos;
        }

        public static Block GetBlock(RaycastHit hit, bool adjacent = false)
        {
            Chunk chunk = hit.collider.GetComponent<Chunk>();
            if (chunk == null) return Blocks.Air;

            WorldPos pos = GetBlockPos(hit, adjacent);
            pos -= chunk.Position;
            return chunk.GetBlock(pos.X, pos.Y, pos.Z);
        }

        public static bool SetBlock(RaycastHit hit, Block block, bool adjacent = false)
        {
            Chunk chunk = hit.collider.GetComponent<Chunk>();
            if (chunk == null) return false;

            WorldPos pos = GetBlockPos(hit, adjacent);
            pos -= chunk.Position;
            chunk.SetBlock(pos.X, pos.Y, pos.Z, block);
            return true;
        }
    }
}

