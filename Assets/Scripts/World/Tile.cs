using UnityEngine;
using System;

namespace Voxel
{
	[Serializable]
	public struct Tile
	{
		public int X;
		public int Y;

		public Tile(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
