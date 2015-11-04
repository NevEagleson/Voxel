using UnityEngine;
using System.Collections.Generic;
using SimplexNoise;

namespace Voxel
{
	public class TerrainGen
	{
		public struct TerrainLayer
		{
			public string Name;
			public Block Block;
			public Block ReplaceBlock;
			public int Min;		
			public int Max;
			public int Level;
			public bool Additive;
			public bool UseNoise;
			public float Frequency;
			public int Amplitude;
			public TerrainLayer(string name, Block block, Block replace, int level, int min = int.MinValue, int max = int.MaxValue, bool additive = true) 
			{
				Name = name; 
				Block = block;
				ReplaceBlock = replace;
				Min = min; 
				Max = max;
				Level = level;
				Additive = additive;
				UseNoise = false;
				Frequency = 0f;
				Amplitude = 0;
			}
			public TerrainLayer(string name, Block block, Block replace, int level, float frequency, int amplitude, bool additive = true)
			{
				Name = name;
				Block = block;
				ReplaceBlock = replace;
				Min = int.MinValue;
				Max = int.MaxValue;
				Additive = additive;
				UseNoise = true;
				Level = level;
				Frequency = frequency;
				Amplitude = amplitude;
			}
		}
		public List<TerrainLayer> Layers;

		public TerrainGen()
		{
			Layers = new List<TerrainLayer>();
			Layers.Add(new TerrainLayer("voxel:layer_mountain", "voxel:stone", "voxel:air", -24, 0.008f, 48, false));
			Layers.Add(new TerrainLayer("voxel:layer_base", "voxel:stone", "voxel:air", 0, 0.05f, 4));
			Layers.Add(new TerrainLayer("voxel:layer_dirt", "voxel:dirt", "voxel:air", 0, 0.04f, 2));
			Layers.Add(new TerrainLayer("voxel:layer_ocean", "voxel:water", "voxel:air", 0, int.MinValue, 0, false));
			Layers.Add(new TerrainLayer("voxel:layer_grass", "voxel:grass", "voxel:air", 1));
		}



		public static int GetNoise(int x, int y, int z, float scale, int max)
		{
			return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
		}

		public Chunk PopulateChunk(Chunk chunk)
		{
			for(int x = 0; x < Constants.ChunkSize; ++x)
			{
				for (int z = 0; z < Constants.ChunkSize; ++z)
				{
					PopulateChunkColumn(chunk, x, z);
				}
			}
			return chunk;
		}

		public Chunk PopulateChunkColumn(Chunk chunk, int x, int z)
		{
			if(Layers.Count == 0) return chunk;
			int groundHeight = int.MinValue;

			foreach (TerrainLayer layer in Layers)
			{
				int layerHeight = layer.Level;
				if (layer.UseNoise)
					layerHeight += GetNoise(chunk.Position.X + x, 0, chunk.Position.Z + z, layer.Frequency, layer.Amplitude);
				if (layer.Additive)
					layerHeight += groundHeight;
				layerHeight = Mathf.Clamp(layerHeight, layer.Min, layer.Max);

				for (int y = 0; y < Constants.ChunkSize; ++y)
				{
					int absoluteY = y + chunk.Position.Y;
					if (absoluteY < layer.Min) continue;
					if (absoluteY > layer.Max) continue;
					Block old = chunk.GetBlock(x, y, z);
					if (old == layer.ReplaceBlock)
					{
						if (absoluteY < layerHeight)
							chunk.SetBlock(x, y, z, layer.Block);
					}
				}

				groundHeight = Mathf.Max(groundHeight, layerHeight);
			}
			return chunk;
		}
		
	}
}
