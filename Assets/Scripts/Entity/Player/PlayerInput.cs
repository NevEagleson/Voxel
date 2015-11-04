using UnityEngine;
using System.Collections;

namespace Voxel
{
	public class PlayerInput : MonoBehaviour
	{
		public float MoveSpeed;
		public float LookSpeed;
		public int ViewRadius = Constants.ChunkLoadRadius;
		public KeyCode LockCursorToggle;
		private bool m_cursorLocked = true;

		public bool ShowViewBounds;

		Vector2 rot;

		// Update is called once per frame
		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
				{
					WorldUtility.SetBlock(hit, "voxel:air");
				}
			}
			if (Input.GetMouseButtonDown(1))
			{
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
				{
					WorldUtility.SetBlock(hit, "voxel:dirt", true);
				}
			}
			if (Input.GetKeyDown(LockCursorToggle))
			{
				m_cursorLocked = !m_cursorLocked;
				Cursor.lockState = m_cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
				Cursor.visible = !m_cursorLocked;
			}

			if (m_cursorLocked)
			{
				rot = new Vector2(rot.x + Input.GetAxis("Mouse X") * LookSpeed,
					rot.y + Input.GetAxis("Mouse Y") * LookSpeed);
				rot.y = Mathf.Clamp(rot.y, -90, 90);

				transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
				transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);
			}
			Vector3 dir = transform.forward;
			dir.y = 0;
			dir.Normalize();
			dir *= Input.GetAxis("Vertical") * MoveSpeed;
			transform.position += dir;

			dir = transform.right;
			dir.y = 0;
			dir.Normalize();
			dir *= Input.GetAxis("Horizontal") * MoveSpeed;
			transform.position += dir;

			transform.position += Vector3.up * Input.GetAxis("Tangential") * MoveSpeed;

			WorldPos currentPos = transform.position;
			WorldManager.CurrentWorld.SetVisibleChunks(currentPos.X, currentPos.Y, currentPos.Z, ViewRadius);
		}

		void OnDrawGizmos()
		{
			if (!ShowViewBounds) return;
			Vector3 pos = transform.position;
			pos.x = WorldUtility.PositionToChunk(pos.x);
			pos.y = WorldUtility.PositionToChunk(pos.y);
			pos.z = WorldUtility.PositionToChunk(pos.z);

			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(pos, new Vector3(Constants.ChunkSize, Constants.ChunkSize, Constants.ChunkSize));

			Gizmos.color = Color.cyan;
			int radius = Constants.ChunkSize * ViewRadius;
			Gizmos.DrawWireCube(pos, new Vector3(radius, radius, radius));
		}
	}
}
