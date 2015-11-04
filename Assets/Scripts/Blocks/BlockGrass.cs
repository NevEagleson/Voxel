using UnityEngine;
using System.Collections;
using Voxel;

public class BlockGrass : Block
{
    protected Tile TopTile, BottomTile, SideTile;

    public BlockGrass() : base("voxel:grass")
    {
        TopTile = new Tile(2, 0);
        BottomTile = new Tile(1, 0);
        SideTile = new Tile(3, 0);
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
        switch (dir)
        {
            case Direction.Up:
                return TopTile;
            case Direction.Down:
                return BottomTile;
            default:
                return SideTile;
        }
    }
}
