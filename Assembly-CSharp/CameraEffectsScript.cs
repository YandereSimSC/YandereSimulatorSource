using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x02000225 RID: 549
public class CameraEffectsScript : MonoBehaviour
{
	// Token: 0x06001210 RID: 4624 RVA: 0x0007F7AC File Offset: 0x0007D9AC
	private void Start()
	{
		this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, 0f);
		this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, 0f);
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0007F844 File Offset: 0x0007DA44
	private void Update()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0f, Time.deltaTime);
			if (this.VibrationTimer == 0f)
			{
				GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
				this.VibrationCheck = false;
			}
		}
		if (this.Streaks.color.a > 0f)
		{
			this.AlarmBloom.bloomIntensity -= Time.deltaTime;
			this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, this.Streaks.color.a - Time.deltaTime);
			if (this.Streaks.color.a <= 0f)
			{
				this.AlarmBloom.enabled = false;
			}
		}
		if (this.MurderStreaks.color.a > 0f)
		{
			this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, this.MurderStreaks.color.a - Time.deltaTime);
		}
		this.EffectStrength = 1f - this.Yandere.Sanity * 0.01f;
		this.Vignette.intensity = Mathf.Lerp(this.Vignette.intensity, this.EffectStrength * 5f, Time.deltaTime);
		this.Vignette.blur = Mathf.Lerp(this.Vignette.blur, this.EffectStrength, Time.deltaTime);
		this.Vignette.chromaticAberration = Mathf.Lerp(this.Vignette.chromaticAberration, this.EffectStrength * 5f, Time.deltaTime);
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x0007FA4C File Offset: 0x0007DC4C
	public void Alarm()
	{
		GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
		this.VibrationCheck = true;
		this.VibrationTimer = 0.1f;
		this.AlarmBloom.bloomIntensity = 1f;
		this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, 1f);
		this.AlarmBloom.enabled = true;
		this.Yandere.Jukebox.SFX.PlayOneShot(this.Noticed);
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x0007FAF8 File Offset: 0x0007DCF8
	public void MurderWitnessed()
	{
		GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
		this.VibrationCheck = true;
		this.VibrationTimer = 0.1f;
		this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, 1f);
		this.Yandere.Jukebox.SFX.PlayOneShot(this.Yandere.Noticed ? this.SenpaiNoticed : this.MurderNoticed);
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x0007FB9C File Offset: 0x0007DD9C
	public void DisableCamera()
	{
		if (!this.OneCamera)
		{
			this.OneCamera = true;
			return;
		}
		this.OneCamera = false;
	}

	// Token: 0x0400152D RID: 5421
	public YandereScript Yandere;

	// Token: 0x0400152E RID: 5422
	public Vignetting Vignette;

	// Token: 0x0400152F RID: 5423
	public UITexture MurderStreaks;

	// Token: 0x04001530 RID: 5424
	public UITexture Streaks;

	// Token: 0x04001531 RID: 5425
	public Bloom AlarmBloom;

	// Token: 0x04001532 RID: 5426
	public float EffectStrength;

	// Token: 0x04001533 RID: 5427
	public float VibrationTimer;

	// Token: 0x04001534 RID: 5428
	public Bloom QualityBloom;

	// Token: 0x04001535 RID: 5429
	public Vignetting QualityVignetting;

	// Token: 0x04001536 RID: 5430
	public AntialiasingAsPostEffect QualityAntialiasingAsPostEffect;

	// Token: 0x04001537 RID: 5431
	public bool VibrationCheck;

	// Token: 0x04001538 RID: 5432
	public bool OneCamera;

	// Token: 0x04001539 RID: 5433
	public AudioClip MurderNoticed;

	// Token: 0x0400153A RID: 5434
	public AudioClip SenpaiNoticed;

	// Token: 0x0400153B RID: 5435
	public AudioClip Noticed;
}
