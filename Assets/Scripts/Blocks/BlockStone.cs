using UnityEngine;
using System.Collections;
using Voxel;

public class BlockStone : Block
{
	protected Tile Tile;

	public BlockStone()
		: base("voxel:stone")
	{
		Tile = new Tile(0, 0);
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
