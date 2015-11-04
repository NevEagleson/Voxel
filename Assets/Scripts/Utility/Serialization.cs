using UnityEngine;
using System.Collections;
using System.IO;

namespace Voxel
{
	public static class Serialization
	{
		public static string SaveLocation(string worldName)
		{
			string saveLocation = Application.persistentDataPath + Path.DirectorySeparatorChar + Constants.SaveFolder + Path.DirectorySeparatorChar + worldName + Path.DirectorySeparatorChar;
			if (!Directory.Exists(saveLocation))
			{
				Directory.CreateDirectory(saveLocation);
			}
			return saveLocation;
		}

		public static string FileName(WorldPos chunkLocation)
		{
			string fileName = string.Format("{0}_{1}_{2}.chunk", chunkLocation.X, chunkLocation.Y, chunkLocation.Z);
			return fileName;
		}

		public static void SaveChunk(Chunk chunk)
		{
			string saveFile = SaveLocation(chunk.World.WorldName);
			saveFile += FileName(chunk.Position);

			Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
			BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.UTF8);
			chunk.Serialize(writer);
			writer.Flush();
			stream.Close();
		}

		public static bool LoadChunk(Chunk chunk)
		{
			string saveFile = SaveLocation(chunk.World.WorldName);
			saveFile += FileName(chunk.Position);

			if (!File.Exists(saveFile))
				return false;

			Stream stream = new FileStream(saveFile, FileMode.Open);
			BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.UTF8);
			chunk.Deserialize(reader);
			stream.Close();
			return true;
		}
	}
}
