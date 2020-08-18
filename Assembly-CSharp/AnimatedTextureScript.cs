using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class AnimatedTextureScript : MonoBehaviour
{
	// Token: 0x060009F9 RID: 2553 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Awake()
	{
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x060009FA RID: 2554 RVA: 0x0004EE3B File Offset: 0x0004D03B
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0004EE4C File Offset: 0x0004D04C
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
		this.MyRenderer.material.mainTexture = this.Image[this.Frame];
	}

	// Token: 0x040009D0 RID: 2512
	[SerializeField]
	private Renderer MyRenderer;

	// Token: 0x040009D1 RID: 2513
	[SerializeField]
	private int Start;

	// Token: 0x040009D2 RID: 2514
	[SerializeField]
	private int Frame;

	// Token: 0x040009D3 RID: 2515
	[SerializeField]
	private int Limit;

	// Token: 0x040009D4 RID: 2516
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x040009D5 RID: 2517
	[SerializeField]
	private float CurrentSeconds;

	// Token: 0x040009D6 RID: 2518
	public Texture[] Image;
}
