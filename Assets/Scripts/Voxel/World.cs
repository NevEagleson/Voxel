using UnityEngine;
using System.Collections.Generic;

namespace Voxel
{
    public class World : MonoBehaviour
    {
        [SerializeField]
        private GameObject ChunkPrefab;

        public Dictionary<WorldPos, Chunk> Chunks { get; private set; }


        // Use this for initialization
        void Start()
        {
            Chunks = new Dictionary<WorldPos, Chunk>();

            CreateChunk(0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private int PositionToChunk(int x)
        {
            return Mathf.FloorToInt(x / (float)Constants.ChunkSize) * Constants.ChunkSize;
        }

        public void CreateChunk(int x, int y, int z)
        {
            x = PositionToChunk(x);
            y = PositionToChunk(y);
            z = PositionToChunk(z);
            WorldPos pos = new WorldPos(x, y, z);
            GameObject chunkObj = Instantiate(ChunkPrefab, new Vector3(x * Constants.ChunkSize, y * Constants.ChunkSize, z * Constants.ChunkSize), Quaternion.identity) as GameObject;
            chunkObj.name = string.Format("Chunk_{0}_{1}_{2}", x, y, z);
            chunkObj.transform.SetParent(transform, true);
            Chunk chunk = chunkObj.GetComponent<Chunk>();
            chunk.Setup(this, pos);

            Chunks.Add(pos, chunk);

            for (int xi = 0; xi < Constants.ChunkSize; xi++)
            {
                for (int yi = 0; yi < Constants.ChunkSize; yi++)
                {
                    for (int zi = 0; zi < Constants.ChunkSize; zi++)
                    {
                        if (yi < 7)
                        {
                            SetBlock(x + xi, y + yi, z + zi, Blocks.Dirt);
                        }
                        else if (yi == 7)
                        {
                            SetBlock(x + xi, y + yi, z + zi, Blocks.Grass);
                        }
                        else
                        {
                            SetBlock(x + xi, y + yi, z + zi, Blocks.Air);
                        }
                    }
                }
            }

        }

        public void DestroyChunk(int x, int y, int z)
        {
            Chunk chunk = GetChunk(x, y, z);
            if (chunk != null)
            {
                Chunks.Remove(chunk.Position);
                Object.Destroy(chunk.gameObject);
            }
        }

        public Chunk GetChunk(int x, int y, int z)
        {
            x = PositionToChunk(x);
            y = PositionToChunk(y);
            z = PositionToChunk(z);
            WorldPos pos = new WorldPos(x, y, z);
            Chunk result = null;
            Chunks.TryGetValue(pos, out result);
            return result;
        }

        public Block GetBlock(int x, int y, int z)
        {
            Chunk chunk = GetChunk(x, y, z);
            if (chunk != null)
            {
                return chunk.GetBlock(x - chunk.Position.X, y - chunk.Position.Y, z - chunk.Position.Z);
            }
            return Blocks.Air;
        }

        public void SetBlock(int x, int y, int z, Block block)
        {
            Chunk chunk = GetChunk(x, y, z);
            if (chunk != null)
            {
                chunk.SetBlock(x - chunk.Position.X, y - chunk.Position.Y, z - chunk.Position.Z, block);
            }
        }
    }
}

