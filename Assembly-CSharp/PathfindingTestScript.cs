using System;
using UnityEngine;

// Token: 0x02000497 RID: 1175
public class PathfindingTestScript : MonoBehaviour
{
	// Token: 0x06001E10 RID: 7696 RVA: 0x0017837C File Offset: 0x0017657C
	private void Update()
	{
		if (Input.GetKeyDown("left"))
		{
			this.bytes = AstarPath.active.astarData.SerializeGraphs();
		}
		if (Input.GetKeyDown("right"))
		{
			AstarPath.active.astarData.DeserializeGraphs(this.bytes);
			AstarPath.active.Scan(null);
		}
	}

	// Token: 0x04003BE5 RID: 15333
	private byte[] bytes;
}
