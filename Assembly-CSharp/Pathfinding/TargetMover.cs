using System;
using System.Linq;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055F RID: 1375
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
	public class TargetMover : MonoBehaviour
	{
		// Token: 0x0600244F RID: 9295 RVA: 0x001971E0 File Offset: 0x001953E0
		public void Start()
		{
			this.cam = Camera.main;
			this.ais = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray<IAstarAI>();
			base.useGUILayout = false;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00197209 File Offset: 0x00195409
		public void OnGUI()
		{
			if (this.onlyOnDoubleClick && this.cam != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x00197240 File Offset: 0x00195440
		private void Update()
		{
			if (!this.onlyOnDoubleClick && this.cam != null)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00197260 File Offset: 0x00195460
		public void UpdateTargetPosition()
		{
			Vector3 vector = Vector3.zero;
			bool flag = false;
			RaycastHit raycastHit;
			if (this.use2D)
			{
				vector = this.cam.ScreenToWorldPoint(Input.mousePosition);
				vector.z = 0f;
				flag = true;
			}
			else if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity, this.mask))
			{
				vector = raycastHit.point;
				flag = true;
			}
			if (flag && vector != this.target.position)
			{
				this.target.position = vector;
				if (this.onlyOnDoubleClick)
				{
					for (int i = 0; i < this.ais.Length; i++)
					{
						if (this.ais[i] != null)
						{
							this.ais[i].SearchPath();
						}
					}
				}
			}
		}

		// Token: 0x0400402E RID: 16430
		public LayerMask mask;

		// Token: 0x0400402F RID: 16431
		public Transform target;

		// Token: 0x04004030 RID: 16432
		private IAstarAI[] ais;

		// Token: 0x04004031 RID: 16433
		public bool onlyOnDoubleClick;

		// Token: 0x04004032 RID: 16434
		public bool use2D;

		// Token: 0x04004033 RID: 16435
		private Camera cam;
	}
}
