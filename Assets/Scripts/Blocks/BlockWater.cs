using UnityEngine;
using System.Collections;
using Voxel;

public class BlockWater : Block
{
	protected Tile Tile;

	public BlockWater()
		: base("voxel:water")
	{
		Tile = new Tile(0, 1);
	}

	public override MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
	{
		return base.Blockdata(chunk, x, y, z, meshData);
	}

	public override bool IsSolid(Direction direction)
	{
		return false;
	}

	public override Tile GetTile(Direction dir)
	{
		return Tile;
	}
}
