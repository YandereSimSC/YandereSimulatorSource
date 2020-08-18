using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class GreenRoomScript : MonoBehaviour
{
	// Token: 0x060016D1 RID: 5841 RVA: 0x000BCC61 File Offset: 0x000BAE61
	private void Start()
	{
		this.QualityManager.Obscurance.enabled = false;
		this.UpdateColor();
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x000BCC7A File Offset: 0x000BAE7A
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.UpdateColor();
		}
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x000BCC90 File Offset: 0x000BAE90
	private void UpdateColor()
	{
		this.ID++;
		if (this.ID > 7)
		{
			this.ID = 0;
		}
		this.Renderers[0].material.color = this.Colors[this.ID];
		this.Renderers[1].material.color = this.Colors[this.ID];
	}

	// Token: 0x04001E16 RID: 7702
	public QualityManagerScript QualityManager;

	// Token: 0x04001E17 RID: 7703
	public Color[] Colors;

	// Token: 0x04001E18 RID: 7704
	public Renderer[] Renderers;

	// Token: 0x04001E19 RID: 7705
	public int ID;
}
