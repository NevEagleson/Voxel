using UnityEngine;
using System.Collections;
using Voxel;

public class BlockDirt : Block
{
    protected Tile Tile;

    public BlockDirt() : base("voxel:dirt")
    {
        Tile = new Tile(1, 0);
    }

    public override MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        return base.Blockdata(chunk, x, y, z, meshData);
    }

    public override bool IsSolid(Direction direction)
    {
        return true;
    }

    public override Tile GetTile(Direction dir)
    {
        return Tile;
    }
}
