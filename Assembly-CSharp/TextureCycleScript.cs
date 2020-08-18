using System;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class TextureCycleScript : MonoBehaviour
{
	// Token: 0x06001C23 RID: 7203 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Awake()
	{
	}

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0014FB7C File Offset: 0x0014DD7C
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x0014FB8C File Offset: 0x0014DD8C
	private void Update()
	{
		this.ID++;
		if (this.ID > 1)
		{
			this.ID = 0;
			this.Frame++;
			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}
		this.Sprite.mainTexture = this.Textures[this.Frame];
	}

	// Token: 0x04003490 RID: 13456
	public UITexture Sprite;

	// Token: 0x04003491 RID: 13457
	[SerializeField]
	private int Start;

	// Token: 0x04003492 RID: 13458
	[SerializeField]
	private int Frame;

	// Token: 0x04003493 RID: 13459
	[SerializeField]
	private int Limit;

	// Token: 0x04003494 RID: 13460
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x04003495 RID: 13461
	[SerializeField]
	private float CurrentSeconds;

	// Token: 0x04003496 RID: 13462
	[SerializeField]
	private Texture[] Textures;

	// Token: 0x04003497 RID: 13463
	public int ID;
}
