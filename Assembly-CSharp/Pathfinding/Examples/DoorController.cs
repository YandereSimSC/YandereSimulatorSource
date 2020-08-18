using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F3 RID: 1523
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_door_controller.php")]
	public class DoorController : MonoBehaviour
	{
		// Token: 0x060029F2 RID: 10738 RVA: 0x001C24FD File Offset: 0x001C06FD
		public void Start()
		{
			this.bounds = base.GetComponent<Collider>().bounds;
			this.SetState(this.open);
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x001C251C File Offset: 0x001C071C
		private void OnGUI()
		{
			if (GUI.Button(new Rect(5f, this.yOffset, 100f, 22f), "Toggle Door"))
			{
				this.SetState(!this.open);
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x001C2554 File Offset: 0x001C0754
		public void SetState(bool open)
		{
			this.open = open;
			if (this.updateGraphsWithGUO)
			{
				GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.bounds);
				int num = open ? this.opentag : this.closedtag;
				if (num > 31)
				{
					Debug.LogError("tag > 31");
					return;
				}
				graphUpdateObject.modifyTag = true;
				graphUpdateObject.setTag = num;
				graphUpdateObject.updatePhysics = false;
				AstarPath.active.UpdateGraphs(graphUpdateObject);
			}
			if (open)
			{
				base.GetComponent<Animation>().Play("Open");
				return;
			}
			base.GetComponent<Animation>().Play("Close");
		}

		// Token: 0x040043BB RID: 17339
		private bool open;

		// Token: 0x040043BC RID: 17340
		public int opentag = 1;

		// Token: 0x040043BD RID: 17341
		public int closedtag = 1;

		// Token: 0x040043BE RID: 17342
		public bool updateGraphsWithGUO = true;

		// Token: 0x040043BF RID: 17343
		public float yOffset = 5f;

		// Token: 0x040043C0 RID: 17344
		private Bounds bounds;
	}
}
