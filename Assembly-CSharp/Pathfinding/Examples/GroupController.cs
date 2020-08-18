using System;
using System.Collections.Generic;
using Pathfinding.RVO;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005E4 RID: 1508
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_group_controller.php")]
	public class GroupController : MonoBehaviour
	{
		// Token: 0x0600299F RID: 10655 RVA: 0x001BF428 File Offset: 0x001BD628
		public void Start()
		{
			this.cam = Camera.main;
			RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
			if (rvosimulator == null)
			{
				base.enabled = false;
				throw new Exception("No RVOSimulator in the scene. Please add one");
			}
			this.sim = rvosimulator.GetSimulator();
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x001BF47C File Offset: 0x001BD67C
		public void Update()
		{
			if (Screen.fullScreen && Screen.width != Screen.resolutions[Screen.resolutions.Length - 1].width)
			{
				Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);
			}
			if (this.adjustCamera)
			{
				List<Agent> agents = this.sim.GetAgents();
				float num = 0f;
				for (int i = 0; i < agents.Count; i++)
				{
					float num2 = Mathf.Max(Mathf.Abs(agents[i].Position.x), Mathf.Abs(agents[i].Position.y));
					if (num2 > num)
					{
						num = num2;
					}
				}
				float a = num / Mathf.Tan(this.cam.fieldOfView * 0.017453292f / 2f);
				float b = num / Mathf.Tan(Mathf.Atan(Mathf.Tan(this.cam.fieldOfView * 0.017453292f / 2f) * this.cam.aspect));
				float num3 = Mathf.Max(a, b) * 1.1f;
				num3 = Mathf.Max(num3, 20f);
				this.cam.transform.position = Vector3.Lerp(this.cam.transform.position, new Vector3(0f, num3, 0f), Time.smoothDeltaTime * 2f);
			}
			if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Mouse0))
			{
				this.Order();
			}
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x001BF61C File Offset: 0x001BD81C
		private void OnGUI()
		{
			if (Event.current.type == EventType.MouseUp && Event.current.button == 0 && !Input.GetKey(KeyCode.A))
			{
				this.Select(this.start, this.end);
				this.wasDown = false;
			}
			if (Event.current.type == EventType.MouseDrag && Event.current.button == 0)
			{
				this.end = Event.current.mousePosition;
				if (!this.wasDown)
				{
					this.start = this.end;
					this.wasDown = true;
				}
			}
			if (Input.GetKey(KeyCode.A))
			{
				this.wasDown = false;
			}
			if (this.wasDown)
			{
				Rect position = Rect.MinMaxRect(Mathf.Min(this.start.x, this.end.x), Mathf.Min(this.start.y, this.end.y), Mathf.Max(this.start.x, this.end.x), Mathf.Max(this.start.y, this.end.y));
				if (position.width > 4f && position.height > 4f)
				{
					GUI.Box(position, "", this.selectionBox);
				}
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x001BF764 File Offset: 0x001BD964
		public void Order()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit))
			{
				float num = 0f;
				for (int i = 0; i < this.selection.Count; i++)
				{
					num += this.selection[i].GetComponent<RVOController>().radius;
				}
				float num2 = num / 3.1415927f;
				num2 *= 2f;
				for (int j = 0; j < this.selection.Count; j++)
				{
					float num3 = 6.2831855f * (float)j / (float)this.selection.Count;
					Vector3 target = raycastHit.point + new Vector3(Mathf.Cos(num3), 0f, Mathf.Sin(num3)) * num2;
					this.selection[j].SetTarget(target);
					this.selection[j].SetColor(this.GetColor(num3));
					this.selection[j].RecalculatePath();
				}
			}
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x001BF878 File Offset: 0x001BDA78
		public void Select(Vector2 _start, Vector2 _end)
		{
			_start.y = (float)Screen.height - _start.y;
			_end.y = (float)Screen.height - _end.y;
			Vector2 vector = Vector2.Min(_start, _end);
			Vector2 vector2 = Vector2.Max(_start, _end);
			if ((vector2 - vector).sqrMagnitude < 16f)
			{
				return;
			}
			this.selection.Clear();
			RVOExampleAgent[] array = UnityEngine.Object.FindObjectsOfType(typeof(RVOExampleAgent)) as RVOExampleAgent[];
			for (int i = 0; i < array.Length; i++)
			{
				Vector2 vector3 = this.cam.WorldToScreenPoint(array[i].transform.position);
				if (vector3.x > vector.x && vector3.y > vector.y && vector3.x < vector2.x && vector3.y < vector2.y)
				{
					this.selection.Add(array[i]);
				}
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x001BF971 File Offset: 0x001BDB71
		public Color GetColor(float angle)
		{
			return AstarMath.HSVToRGB(angle * 57.295776f, 0.8f, 0.6f);
		}

		// Token: 0x0400434E RID: 17230
		public GUIStyle selectionBox;

		// Token: 0x0400434F RID: 17231
		public bool adjustCamera = true;

		// Token: 0x04004350 RID: 17232
		private Vector2 start;

		// Token: 0x04004351 RID: 17233
		private Vector2 end;

		// Token: 0x04004352 RID: 17234
		private bool wasDown;

		// Token: 0x04004353 RID: 17235
		private List<RVOExampleAgent> selection = new List<RVOExampleAgent>();

		// Token: 0x04004354 RID: 17236
		private Simulator sim;

		// Token: 0x04004355 RID: 17237
		private Camera cam;

		// Token: 0x04004356 RID: 17238
		private const float rad2Deg = 57.295776f;
	}
}
