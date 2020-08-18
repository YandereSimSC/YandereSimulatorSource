using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F7 RID: 1527
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_object_placer.php")]
	public class ObjectPlacer : MonoBehaviour
	{
		// Token: 0x060029FF RID: 10751 RVA: 0x001C28B8 File Offset: 0x001C0AB8
		private void Update()
		{
			if (Input.GetKeyDown("p"))
			{
				this.PlaceObject();
			}
			if (Input.GetKeyDown("r"))
			{
				this.RemoveObject();
			}
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x001C28E0 File Offset: 0x001C0AE0
		public void PlaceObject()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity))
			{
				Vector3 point = raycastHit.point;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.go, point, Quaternion.identity);
				if (this.issueGUOs)
				{
					GraphUpdateObject ob = new GraphUpdateObject(gameObject.GetComponent<Collider>().bounds);
					AstarPath.active.UpdateGraphs(ob);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
			}
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x001C295C File Offset: 0x001C0B5C
		public void RemoveObject()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity))
			{
				if (raycastHit.collider.isTrigger || raycastHit.transform.gameObject.name == "Ground")
				{
					return;
				}
				Bounds bounds = raycastHit.collider.bounds;
				UnityEngine.Object.Destroy(raycastHit.collider);
				UnityEngine.Object.Destroy(raycastHit.collider.gameObject);
				if (this.issueGUOs)
				{
					GraphUpdateObject ob = new GraphUpdateObject(bounds);
					AstarPath.active.UpdateGraphs(ob);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
			}
		}

		// Token: 0x040043CF RID: 17359
		public GameObject go;

		// Token: 0x040043D0 RID: 17360
		public bool direct;

		// Token: 0x040043D1 RID: 17361
		public bool issueGUOs = true;
	}
}
