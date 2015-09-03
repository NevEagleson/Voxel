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
        Block[, ,] Blocks = new Block[Constants.ChunkSize, Constants.ChunkSize, Constants.ChunkSize];      

        MeshFilter filter;
        MeshCollider coll;

        public World World { get; private set; }
        public WorldPos Position { get; private set; }
        public bool IsDirty { get; set; }

        //Use this for initialization
        void Start()
        {
            filter = gameObject.GetComponent<MeshFilter>();
            coll = gameObject.GetComponent<MeshCollider>();
        }

        //Update is called once per frame
        void Update()
        {
            if (IsDirty)
                UpdateChunk();
        }

        public void Setup(World world, WorldPos pos)
        {
            World = world;
            Position = pos;
            IsDirty = true;
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
                return Blocks[x, y, z];
            return World.GetBlock(x + Position.X, y + Position.Y, z + Position.Z);
        }

        public void SetBlock(int x, int y, int z, Block block, bool updateChunk = true)
        {
            if (WithinChunk(x, y, z))
            {
                Blocks[x, y, z] = block;
                if(updateChunk)
                    IsDirty = true;
            }
            else
                World.SetBlock(x + Position.X, y + Position.Y, z + Position.Z, block);

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
                        meshData = Blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                    }
                }
            }

            RenderMesh(meshData);
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

    }

}