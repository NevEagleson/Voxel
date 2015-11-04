using UnityEngine;
using System.Collections.Generic;

namespace Voxel
{
    public static class Blocks
    {
        public static Block Air = new BlockAir();
		public static Block Stone = new BlockStone();
        public static Block Dirt = new BlockDirt();
        public static Block Grass = new BlockGrass();
		public static Block Water = new BlockWater();

        public static Dictionary<int, Block> All = new Dictionary<int, Block>();
        public static Dictionary<int, Block> Solid = new Dictionary<int, Block>();
        public static Dictionary<int, Block> Liquid = new Dictionary<int, Block>();
        public static Dictionary<int, Block> Updating = new Dictionary<int, Block>();

        static Blocks()
        {
            LoadBlocks();
        }

        public static void LoadBlocks()
        {
			RegisterBlock(Air);

			RegisterSolidBlock(Stone);
			RegisterSolidBlock(Dirt);
			RegisterSolidBlock(Grass);

			RegisterLiquidBlock(Water);
        }

		public static void RegisterBlock(Block block)
		{
			All.Add(block.Name.GetHashCode(), block);
		}
		public static void RegisterSolidBlock(Block block)
		{
			All.Add(block.Name.GetHashCode(), block);
			Solid.Add(block.Name.GetHashCode(), block);
		}
		public static void RegisterLiquidBlock(Block block)
		{
			All.Add(block.Name.GetHashCode(), block);
			Liquid.Add(block.Name.GetHashCode(), block);
		}
		public static void RegisterUpdatingBlock(Block block)
		{
			Updating.Add(block.Name.GetHashCode(), block);
		}
    }
}

