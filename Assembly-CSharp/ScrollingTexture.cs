using System;
using UnityEngine;

// Token: 0x020003CE RID: 974
public class ScrollingTexture : MonoBehaviour
{
	// Token: 0x06001A52 RID: 6738 RVA: 0x00101A73 File Offset: 0x000FFC73
	private void Start()
	{
		this.MyRenderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x00101A84 File Offset: 0x000FFC84
	private void Update()
	{
		this.Offset += Time.deltaTime * this.Speed;
		this.MyRenderer.material.SetTextureOffset("_MainTex", new Vector2(this.Offset, this.Offset));
	}

	// Token: 0x040029B2 RID: 10674
	public Renderer MyRenderer;

	// Token: 0x040029B3 RID: 10675
	public float Offset;

	// Token: 0x040029B4 RID: 10676
	public float Speed;
}
