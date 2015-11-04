using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Voxel
{
    public class World : MonoBehaviour
    {
        [SerializeField]
        private GameObject ChunkPrefab;

        public Dictionary<WorldPos, Chunk> Chunks { get; private set; }

		public string WorldName = "World";

		public TerrainGen Generator;

		public bool AlwaysGenerateChunks;
		public bool DontDestroyOldChunks;

		private List<WorldPos> m_chunksToCreate;

        // Use this for initialization
        void Start()
        {
            Chunks = new Dictionary<WorldPos, Chunk>();
			m_chunksToCreate = new List<WorldPos>();

			Generator = new TerrainGen();
        }

        // Update is called once per frame
        void Update()
        {
			if (m_chunksToCreate.Count > 0)
			{
				WorldPos newChunkPos = m_chunksToCreate[m_chunksToCreate.Count - 1];
				m_chunksToCreate.RemoveAt(m_chunksToCreate.Count - 1);
				Chunk c = GetChunk(newChunkPos.X, newChunkPos.Y, newChunkPos.Z);
				if (c != null)
				{
					c.IsVisible = true;
					c.IsDead = false;
				}
				else
					CreateChunk(newChunkPos.X, newChunkPos.Y, newChunkPos.Z);
			}

			if (DontDestroyOldChunks) return;
			Chunk chunk2Delete = null;
			foreach (Chunk chunk in Chunks.Values)
			{
				if (chunk.IsDead)
				{
					chunk2Delete = chunk;
					break;
				}
			}
			if (chunk2Delete != null)
				DestroyChunk(chunk2Delete);
        }

        private int PositionToChunk(int x)
        {
            return Mathf.FloorToInt(x / (float)Constants.ChunkSize) * Constants.ChunkSize;
        }

		public void SetVisibleChunks(int x, int y, int z, int radius)
		{
			x = PositionToChunk(x);
			y = PositionToChunk(y);
			z = PositionToChunk(z);

			int size = Constants.ChunkSize * radius;
			WorldPos minPos = new WorldPos(x - size, y - size, z - size);
			WorldPos maxPos = new WorldPos(x + Constants.ChunkSize + size, y + Constants.ChunkSize + size, z + Constants.ChunkSize + size);


			foreach (Chunk chunk in Chunks.Values)
			{
				if (chunk.Position.X < minPos.X || chunk.Position.X > maxPos.X)
				{
					chunk.IsVisible = false;
					chunk.IsDead = true;
					continue;
				}
				if (chunk.Position.Y < minPos.Y || chunk.Position.Y > maxPos.Y)
				{
					chunk.IsVisible = false;
					chunk.IsDead = true;
					continue;
				}
				if (chunk.Position.Z < minPos.Z || chunk.Position.Z > maxPos.Z)
				{
					chunk.IsVisible = false;
					chunk.IsDead = true;
					continue;
				}
			}

			for (int xi = minPos.X; xi <= maxPos.X; xi += Constants.ChunkSize)
			{
				for (int yi = minPos.Y; yi <= maxPos.Y; yi += Constants.ChunkSize)
				{
					for (int zi = minPos.Z; zi <= maxPos.Z; zi += Constants.ChunkSize)
					{
						m_chunksToCreate.Add(new WorldPos(xi, yi, zi));
					}
				}
			}
		}

		public void CreateChunk(int x, int y, int z)
        {
            x = PositionToChunk(x);
            y = PositionToChunk(y);
            z = PositionToChunk(z);
            WorldPos pos = new WorldPos(x, y, z);
            GameObject chunkObj = Instantiate(ChunkPrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
            chunkObj.name = string.Format("Chunk_{0}_{1}_{2}", x, y, z);
            chunkObj.transform.SetParent(transform, true);
            Chunk chunk = chunkObj.GetComponent<Chunk>();
            chunk.Setup(this, pos);

            Chunks.Add(pos, chunk);

			if (AlwaysGenerateChunks)
				Generator.PopulateChunk(chunk);
			else
			{
				bool loaded = Serialization.LoadChunk(chunk);
				if (!loaded)
					Generator.PopulateChunk(chunk);
			}
        }

		public void DestroyChunk(Chunk chunk, bool save = true)
        {
            if (chunk != null)
            {
				chunk.SaveOnDestruction = save;
                Chunks.Remove(chunk.Position);
                Object.Destroy(chunk.gameObject);
            }
        }

		public void PopulateChunk(int x, int y, int z)
		{
			x = PositionToChunk(x);
			y = PositionToChunk(y);
			z = PositionToChunk(z);
			
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

        public void SetBlock(int x, int y, int z, Block block, bool updateChunk = true)
        {
            Chunk chunk = GetChunk(x, y, z);
            if (chunk != null)
            {
                chunk.SetBlock(x - chunk.Position.X, y - chunk.Position.Y, z - chunk.Position.Z, block, updateChunk);
            }
        }

		public void SetChunkDirty(int x, int y, int z)
		{
			 Chunk chunk = GetChunk(x, y, z);
			 if (chunk != null)
			 {
				 chunk.IsDirty = true;
			 }
		}
    }
}

