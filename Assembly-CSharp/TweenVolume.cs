using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x170000DC RID: 220
	// (get) Token: 0x0600061E RID: 1566 RVA: 0x00035ABC File Offset: 0x00033CBC
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.GetComponent<AudioSource>();
				if (this.mSource == null)
				{
					this.mSource = base.GetComponent<AudioSource>();
					if (this.mSource == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x0600061F RID: 1567 RVA: 0x00035B23 File Offset: 0x00033D23
	// (set) Token: 0x06000620 RID: 1568 RVA: 0x00035B2B File Offset: 0x00033D2B
	[Obsolete("Use 'value' instead")]
	public float volume
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000621 RID: 1569 RVA: 0x00035B34 File Offset: 0x00033D34
	// (set) Token: 0x06000622 RID: 1570 RVA: 0x00035B55 File Offset: 0x00033D55
	public float value
	{
		get
		{
			if (!(this.audioSource != null))
			{
				return 0f;
			}
			return this.mSource.volume;
		}
		set
		{
			if (this.audioSource != null)
			{
				this.mSource.volume = value;
			}
		}
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x00035B71 File Offset: 0x00033D71
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00035BB0 File Offset: 0x00033DB0
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration, 0f);
		tweenVolume.from = tweenVolume.value;
		tweenVolume.to = targetVolume;
		if (targetVolume > 0f)
		{
			AudioSource audioSource = tweenVolume.audioSource;
			audioSource.enabled = true;
			audioSource.Play();
		}
		return tweenVolume;
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00035BF8 File Offset: 0x00033DF8
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00035C06 File Offset: 0x00033E06
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x040005F8 RID: 1528
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x040005F9 RID: 1529
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x040005FA RID: 1530
	private AudioSource mSource;
}
