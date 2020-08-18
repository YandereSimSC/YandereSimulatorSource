using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class UI2DSpriteAnimation : MonoBehaviour
{
	// Token: 0x17000124 RID: 292
	// (get) Token: 0x060006DD RID: 1757 RVA: 0x000144F8 File Offset: 0x000126F8
	public bool isPlaying
	{
		get
		{
			return base.enabled;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x060006DE RID: 1758 RVA: 0x000384FF File Offset: 0x000366FF
	// (set) Token: 0x060006DF RID: 1759 RVA: 0x00038507 File Offset: 0x00036707
	public int framesPerSecond
	{
		get
		{
			return this.framerate;
		}
		set
		{
			this.framerate = value;
		}
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00038510 File Offset: 0x00036710
	public void Play()
	{
		if (this.frames != null && this.frames.Length != 0)
		{
			if (!base.enabled && !this.loop)
			{
				int num = (this.framerate > 0) ? (this.frameIndex + 1) : (this.frameIndex - 1);
				if (num < 0 || num >= this.frames.Length)
				{
					this.frameIndex = ((this.framerate < 0) ? (this.frames.Length - 1) : 0);
				}
			}
			base.enabled = true;
			this.UpdateSprite();
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00038592 File Offset: 0x00036792
	public void Pause()
	{
		base.enabled = false;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0003859B File Offset: 0x0003679B
	public void ResetToBeginning()
	{
		this.frameIndex = ((this.framerate < 0) ? (this.frames.Length - 1) : 0);
		this.UpdateSprite();
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x000385BF File Offset: 0x000367BF
	private void Start()
	{
		this.Play();
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x000385C8 File Offset: 0x000367C8
	private void Update()
	{
		if (this.frames == null || this.frames.Length == 0)
		{
			base.enabled = false;
			return;
		}
		if (this.framerate != 0)
		{
			float num = this.ignoreTimeScale ? RealTime.time : Time.time;
			if (this.mUpdate < num)
			{
				this.mUpdate = num;
				int num2 = (this.framerate > 0) ? (this.frameIndex + 1) : (this.frameIndex - 1);
				if (!this.loop && (num2 < 0 || num2 >= this.frames.Length))
				{
					base.enabled = false;
					return;
				}
				this.frameIndex = NGUIMath.RepeatIndex(num2, this.frames.Length);
				this.UpdateSprite();
			}
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00038670 File Offset: 0x00036870
	private void UpdateSprite()
	{
		if (this.mUnitySprite == null && this.mNguiSprite == null)
		{
			this.mUnitySprite = base.GetComponent<SpriteRenderer>();
			this.mNguiSprite = base.GetComponent<UI2DSprite>();
			if (this.mUnitySprite == null && this.mNguiSprite == null)
			{
				base.enabled = false;
				return;
			}
		}
		float num = this.ignoreTimeScale ? RealTime.time : Time.time;
		if (this.framerate != 0)
		{
			this.mUpdate = num + Mathf.Abs(1f / (float)this.framerate);
		}
		if (this.mUnitySprite != null)
		{
			this.mUnitySprite.sprite = this.frames[this.frameIndex];
			return;
		}
		if (this.mNguiSprite != null)
		{
			this.mNguiSprite.nextSprite = this.frames[this.frameIndex];
		}
	}

	// Token: 0x0400062F RID: 1583
	public int frameIndex;

	// Token: 0x04000630 RID: 1584
	[SerializeField]
	protected int framerate = 20;

	// Token: 0x04000631 RID: 1585
	public bool ignoreTimeScale = true;

	// Token: 0x04000632 RID: 1586
	public bool loop = true;

	// Token: 0x04000633 RID: 1587
	public Sprite[] frames;

	// Token: 0x04000634 RID: 1588
	private SpriteRenderer mUnitySprite;

	// Token: 0x04000635 RID: 1589
	private UI2DSprite mNguiSprite;

	// Token: 0x04000636 RID: 1590
	private float mUpdate;
}
