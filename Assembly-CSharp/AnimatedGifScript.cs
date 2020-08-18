using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class AnimatedGifScript : MonoBehaviour
{
	// Token: 0x060009F5 RID: 2549 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Awake()
	{
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0004EDA2 File Offset: 0x0004CFA2
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0004EDB0 File Offset: 0x0004CFB0
	private void Update()
	{
		this.CurrentSeconds += Time.unscaledDeltaTime;
		while (this.CurrentSeconds >= this.SecondsPerFrame)
		{
			this.CurrentSeconds -= this.SecondsPerFrame;
			this.Frame++;
			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}
		this.Sprite.spriteName = this.SpriteName + this.Frame.ToString();
	}

	// Token: 0x040009C9 RID: 2505
	[SerializeField]
	private UISprite Sprite;

	// Token: 0x040009CA RID: 2506
	[SerializeField]
	private string SpriteName;

	// Token: 0x040009CB RID: 2507
	[SerializeField]
	private int Start;

	// Token: 0x040009CC RID: 2508
	[SerializeField]
	private int Frame;

	// Token: 0x040009CD RID: 2509
	[SerializeField]
	private int Limit;

	// Token: 0x040009CE RID: 2510
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x040009CF RID: 2511
	[SerializeField]
	private float CurrentSeconds;
}
