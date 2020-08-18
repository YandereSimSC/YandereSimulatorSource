using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class GraphUpdaterScript : MonoBehaviour
{
	// Token: 0x060016CF RID: 5839 RVA: 0x000BCC36 File Offset: 0x000BAE36
	private void Update()
	{
		if (this.Frames > 0)
		{
			this.Graph.Scan(null);
			UnityEngine.Object.Destroy(this);
		}
		this.Frames++;
	}

	// Token: 0x04001E14 RID: 7700
	public AstarPath Graph;

	// Token: 0x04001E15 RID: 7701
	public int Frames;
}
