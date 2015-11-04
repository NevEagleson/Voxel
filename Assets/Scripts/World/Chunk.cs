//Chunk.cs
using UnityEngine;
using System.Collections;

namespace Voxel
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        BlockData[, ,] Blocks = new BlockData[Constants.ChunkSize, Constants.ChunkSize, Constants.ChunkSize];      

        MeshFilter filter;
        MeshCollider coll;

        public World World { get; private set; }
        public WorldPos Position { get; private set; }
        public bool IsDirty { get; set; }
		public bool IsVisible { get; set; }
		public bool IsDead { get; set; }
		public bool SaveOnDestruction { get; set; }

        //Use this for initialization
        void Start()
        {
            filter = gameObject.GetComponent<MeshFilter>();
            coll = gameObject.GetComponent<MeshCollider>();
        }

		void OnDestroy()
		{
			if (SaveOnDestruction)
			{
				Serialization.SaveChunk(this);
			}
		}

        //Update is called once per frame
        void Update()
        {
            if (IsDirty)
                UpdateChunk();
        }

		void OnDrawGizmosSelected()
		{
			Gizmos.color = IsVisible ? Color.white : IsDead ? Color.yellow : Color.red;
			Vector3 pos = transform.position;
			Vector3 size = new Vector3(Constants.ChunkSize, Constants.ChunkSize, Constants.ChunkSize);
			pos += size * 0.5f;
			Gizmos.DrawWireCube(pos, size);
		}

        public void Setup(World world, WorldPos pos)
        {
            World = world;
            Position = pos;
            IsDirty = true;
			IsDead = false;
			SaveOnDestruction = true;
        }

        public static bool WithinChunk(int x, int y, int z)
        {
            return x >= 0 && x < Constants.ChunkSize &&
                y >= 0 && y < Constants.ChunkSize &&
                z >= 0 && z < Constants.ChunkSize;
        }

        public Block GetBlock(int x, int y, int z)
        {
            if(WithinChunk(x, y, z))
                return Blocks[x, y, z].Block;
            return World.GetBlock(x + Position.X, y + Position.Y, z + Position.Z);
        }

        public void SetBlock(int x, int y, int z, Block block, bool updateChunk = true)
        {
            if (WithinChunk(x, y, z))
            {
                Blocks[x, y, z].Block = block;
				if (updateChunk)
				{
					IsDirty = true;
					if (x == 0)
						World.SetChunkDirty(Position.X - 1, Position.Y, Position.Z);
					else if (x == Constants.ChunkLast)
						World.SetChunkDirty(Position.X + 1, Position.Y, Position.Z);
					if (y == 0)
						World.SetChunkDirty(Position.X, Position.Y - 1, Position.Z);
					else if (y == Constants.ChunkLast)
						World.SetChunkDirty(Position.X, Position.Y + 1, Position.Z);
					if (z == 0)
						World.SetChunkDirty(Position.X, Position.Y, Position.Z - 1);
					else if (z == Constants.ChunkLast)
						World.SetChunkDirty(Position.X, Position.Y, Position.Z + 1);
				}
            }
            else
                World.SetBlock(x + Position.X, y + Position.Y, z + Position.Z, block, updateChunk);

        }

        //Updates the chunk based on its contents
        void UpdateChunk()
        {
            MeshData meshData = new MeshData();

            for (int x = 0; x < Constants.ChunkSize; x++)
            {
                for (int y = 0; y < Constants.ChunkSize; y++)
                {
                    for (int z = 0; z < Constants.ChunkSize; z++)
                    {
                        meshData = Blocks[x, y, z].Block.Blockdata(this, x, y, z, meshData);
                    }
                }
            }

            RenderMesh(meshData);

			IsVisible = !IsDead;

			IsDirty = false;
        }

        // Sends the calculated mesh information
        // to the mesh and collision components
        void RenderMesh(MeshData meshData)
        {
            filter.mesh.Clear();
            filter.mesh.vertices = meshData.Vertices.ToArray();
            filter.mesh.triangles = meshData.Triangles.ToArray();
            filter.mesh.uv = meshData.UV.ToArray();
            filter.mesh.RecalculateNormals();

            coll.sharedMesh = null;
            Mesh mesh = new Mesh();
            mesh.vertices = meshData.ColVertices.ToArray();
            mesh.triangles = meshData.ColTriangles.ToArray();
            mesh.RecalculateNormals();
            coll.sharedMesh = mesh;
        }

		public void Serialize(System.IO.BinaryWriter stream)
		{
			for (int x = 0; x < Constants.ChunkSize; x++)
			{
				for (int y = 0; y < Constants.ChunkSize; y++)
				{
					for (int z = 0; z < Constants.ChunkSize; z++)
					{
						BlockData block = Blocks[x, y, z];
						stream.Write(block.BlockType);
						stream.Write(block.ByteData);
					}
				}
			}
		}

		public void Deserialize(System.IO.BinaryReader stream)
		{
			for (int x = 0; x < Constants.ChunkSize; x++)
			{
				for (int y = 0; y < Constants.ChunkSize; y++)
				{
					for (int z = 0; z < Constants.ChunkSize; z++)
					{
						BlockData block = Blocks[x, y, z];
						block.BlockType = stream.Read();
						block.ByteData = stream.Read();
					}
				}
			}
		}

    }

}