using UnityEngine;
using System.Collections;

namespace Voxel
{
	public static class Constants
	{
		//World
		public const int ChunkSize = 16;
		public const int ChunkLast = ChunkSize - 1;
		public const float TileSize = 0.25f;
		public const int ChunkLoadRadius = 1;

		//Serialization
		public const string SaveFolder = "VoxelSaves";

		//Player
	}
}
