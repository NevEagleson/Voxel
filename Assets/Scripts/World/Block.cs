using UnityEngine;
using System.Collections;

namespace Voxel
{
    public abstract class Block
    {
        public readonly string Name;

        //Base block constructor
        public Block(string name)
        {
            Name = name.ToLower();
        }

        public virtual MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.Down))
            {
                meshData = FaceDataUp(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.Up))
            {
                meshData = FaceDataDown(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.South))
            {
                meshData = FaceDataNorth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.North))
            {
                meshData = FaceDataSouth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.West))
            {
                meshData = FaceDataEast(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.East))
            {
                meshData = FaceDataWest(chunk, x, y, z, meshData);
            }

            return meshData;
        }


        public virtual bool IsSolid(Direction dir)
        {
            return true;
        }

        public virtual Tile GetTile(Direction dir)
        {
            return new Tile();
        }

        protected virtual MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Direction.Up));

            return meshData;
        }
        protected virtual MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Direction.Down));

            return meshData;
        }
        protected virtual MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Direction.North));

            return meshData;
        }
        protected virtual MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Direction.East));

            return meshData;
        }
        protected virtual MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Direction.South));

            return meshData;
        }
        protected virtual MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddQuadTriangles();
            meshData.UV.AddRange(FaceUVs(Direction.West));

            return meshData;
        }
        protected virtual Vector2[] FaceUVs(Direction dir)
        {
            Vector2[] uvs = new Vector2[4];
            Tile tilePos = GetTile(dir);

            uvs[0] = new Vector2(Constants.TileSize * tilePos.X + Constants.TileSize, Constants.TileSize * tilePos.Y);
            uvs[1] = new Vector2(Constants.TileSize * tilePos.X + Constants.TileSize, Constants.TileSize * tilePos.Y + Constants.TileSize);
            uvs[2] = new Vector2(Constants.TileSize * tilePos.X, Constants.TileSize * tilePos.Y + Constants.TileSize);
            uvs[3] = new Vector2(Constants.TileSize * tilePos.X, Constants.TileSize * tilePos.Y);

            return uvs;
        }

        public static implicit operator Block(string name)
        {
            Block ret;
            if(Blocks.All.TryGetValue(name.ToLower().GetHashCode(), out ret))
            {
                return ret;
            }
            return Blocks.Air;
        }
    }
}
