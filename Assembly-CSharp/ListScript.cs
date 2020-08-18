using System;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class ListScript : MonoBehaviour
{
	// Token: 0x060017ED RID: 6125 RVA: 0x000D2C30 File Offset: 0x000D0E30
	public void Start()
	{
		if (this.AutoFill)
		{
			for (int i = 1; i < this.List.Length; i++)
			{
				this.List[i] = base.transform.GetChild(i - 1);
			}
		}
	}

	// Token: 0x04002259 RID: 8793
	public Transform[] List;

	// Token: 0x0400225A RID: 8794
	public bool AutoFill;
}
