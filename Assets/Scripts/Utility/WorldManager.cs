using UnityEngine;
using System.Collections.Generic;

namespace Voxel
{
public class WorldManager : MonoBehaviour
{
	private static WorldManager s_instance;
	public static WorldManager Instance
	{
		get
		{
			if (s_instance == null)
			{
				WorldManager[] managers = GameObject.FindObjectsOfType<WorldManager>();
				if (managers.Length > 1)
				{
					Debug.Log("Too many instances of WorldManager - remove them from scene");
				}
				if (managers.Length > 0)
				{
					s_instance = managers[0];
					GameObject.DontDestroyOnLoad(s_instance.gameObject);
				}
				else
				{
					GameObject managerObject = new GameObject("WorldManager");
					s_instance = managerObject.AddComponent<WorldManager>();
				}
			}
			return s_instance;
		}
	}

	private static List<World> s_worlds;

	private World m_currentWorld;
	public static World CurrentWorld
	{
		get
		{
			if (Instance.m_currentWorld == null)
			{
				World[] worlds = s_instance.GetComponentsInChildren<World>();
				if (worlds.Length > 0)
					s_instance.m_currentWorld = worlds[0];
				else
				{
					GameObject worldObject = new GameObject("World");
					s_instance.m_currentWorld = worldObject.AddComponent<World>();
				}
				s_worlds.Add(s_instance.m_currentWorld);
			}
			return s_instance.m_currentWorld;
		}
	}

	void Awake()
	{
		s_worlds = new List<World>();
	}
}
}
