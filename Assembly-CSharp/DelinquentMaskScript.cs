using System;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class DelinquentMaskScript : MonoBehaviour
{
	// Token: 0x0600131E RID: 4894 RVA: 0x0009E5C4 File Offset: 0x0009C7C4
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 0;
			}
			this.MyRenderer.mesh = this.Meshes[this.ID];
		}
	}

	// Token: 0x0400197C RID: 6524
	public MeshFilter MyRenderer;

	// Token: 0x0400197D RID: 6525
	public Mesh[] Meshes;

	// Token: 0x0400197E RID: 6526
	public int ID;
}
