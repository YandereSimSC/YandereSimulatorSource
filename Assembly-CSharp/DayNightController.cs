using System;
using UnityEngine;

// Token: 0x0200025A RID: 602
public class DayNightController : MonoBehaviour
{
	// Token: 0x06001306 RID: 4870 RVA: 0x0009B670 File Offset: 0x00099870
	private void Initialize()
	{
		this.quarterDay = this.dayCycleLength * 0.25f;
		this.dawnTime = 0f;
		this.dayTime = this.dawnTime + this.quarterDay;
		this.duskTime = this.dayTime + this.quarterDay;
		this.nightTime = this.duskTime + this.quarterDay;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			this.lightIntensity = component.intensity;
		}
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x0009B6F0 File Offset: 0x000998F0
	private void Reset()
	{
		this.dayCycleLength = 120f;
		this.hoursPerDay = 24f;
		this.dawnTimeOffset = 3f;
		this.fullDark = new Color(0.1254902f, 0.10980392f, 0.18039216f);
		this.fullLight = new Color(0.99215686f, 0.972549f, 0.8745098f);
		this.dawnDuskFog = new Color(0.52156866f, 0.4862745f, 0.4f);
		this.dayFog = new Color(0.7058824f, 0.8156863f, 0.81960785f);
		this.nightFog = new Color(0.047058824f, 0.05882353f, 0.35686275f);
		foreach (Skybox skybox in Resources.FindObjectsOfTypeAll<Skybox>())
		{
			if (skybox.name == "DawnDusk Skybox")
			{
				this.dawnDuskSkybox = skybox.material;
			}
			else if (skybox.name == "StarryNight Skybox")
			{
				this.nightSkybox = skybox.material;
			}
			else if (skybox.name == "Sunny2 Skybox")
			{
				this.daySkybox = skybox.material;
			}
		}
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0009B816 File Offset: 0x00099A16
	private void Start()
	{
		this.Initialize();
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0009B820 File Offset: 0x00099A20
	private void Update()
	{
		if (this.currentCycleTime > this.nightTime && this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			this.SetNight();
		}
		else if (this.currentCycleTime > this.duskTime && this.currentPhase == DayNightController.DayPhase.Day)
		{
			this.SetDusk();
		}
		else if (this.currentCycleTime > this.dayTime && this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			this.SetDay();
		}
		else if (this.currentCycleTime > this.dawnTime && this.currentCycleTime < this.dayTime && this.currentPhase == DayNightController.DayPhase.Night)
		{
			this.SetDawn();
		}
		this.UpdateWorldTime();
		this.UpdateDaylight();
		this.UpdateFog();
		this.currentCycleTime += Time.deltaTime;
		this.currentCycleTime %= this.dayCycleLength;
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x0009B8EC File Offset: 0x00099AEC
	public void SetDawn()
	{
		RenderSettings.skybox = this.dawnDuskSkybox;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.enabled = true;
		}
		this.currentPhase = DayNightController.DayPhase.Dawn;
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x0009B924 File Offset: 0x00099B24
	public void SetDay()
	{
		RenderSettings.skybox = this.daySkybox;
		RenderSettings.ambientLight = this.fullLight;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.intensity = this.lightIntensity;
		}
		this.currentPhase = DayNightController.DayPhase.Day;
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x0009B96A File Offset: 0x00099B6A
	public void SetDusk()
	{
		RenderSettings.skybox = this.dawnDuskSkybox;
		this.currentPhase = DayNightController.DayPhase.Dusk;
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x0009B980 File Offset: 0x00099B80
	public void SetNight()
	{
		RenderSettings.skybox = this.nightSkybox;
		RenderSettings.ambientLight = this.fullDark;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.enabled = false;
		}
		this.currentPhase = DayNightController.DayPhase.Night;
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x0009B9C4 File Offset: 0x00099BC4
	private void UpdateDaylight()
	{
		if (this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			float num = this.currentCycleTime - this.dawnTime;
			RenderSettings.ambientLight = Color.Lerp(this.fullDark, this.fullLight, num / this.quarterDay);
			Light component = base.GetComponent<Light>();
			if (component != null)
			{
				component.intensity = this.lightIntensity * (num / this.quarterDay);
			}
		}
		else if (this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			float num2 = this.currentCycleTime - this.duskTime;
			RenderSettings.ambientLight = Color.Lerp(this.fullLight, this.fullDark, num2 / this.quarterDay);
			Light component2 = base.GetComponent<Light>();
			if (component2 != null)
			{
				component2.intensity = this.lightIntensity * ((this.quarterDay - num2) / this.quarterDay);
			}
		}
		base.transform.Rotate(Vector3.up * (Time.deltaTime / this.dayCycleLength * 360f), Space.Self);
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0009BAB8 File Offset: 0x00099CB8
	private void UpdateFog()
	{
		if (this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			float num = this.currentCycleTime - this.dawnTime;
			RenderSettings.fogColor = Color.Lerp(this.dawnDuskFog, this.dayFog, num / this.quarterDay);
			return;
		}
		if (this.currentPhase == DayNightController.DayPhase.Day)
		{
			float num2 = this.currentCycleTime - this.dayTime;
			RenderSettings.fogColor = Color.Lerp(this.dayFog, this.dawnDuskFog, num2 / this.quarterDay);
			return;
		}
		if (this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			float num3 = this.currentCycleTime - this.duskTime;
			RenderSettings.fogColor = Color.Lerp(this.dawnDuskFog, this.nightFog, num3 / this.quarterDay);
			return;
		}
		if (this.currentPhase == DayNightController.DayPhase.Night)
		{
			float num4 = this.currentCycleTime - this.nightTime;
			RenderSettings.fogColor = Color.Lerp(this.nightFog, this.dawnDuskFog, num4 / this.quarterDay);
		}
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x0009BB9B File Offset: 0x00099D9B
	private void UpdateWorldTime()
	{
		this.worldTimeHour = (int)((Mathf.Ceil(this.currentCycleTime / this.dayCycleLength * this.hoursPerDay) + this.dawnTimeOffset) % this.hoursPerDay) + 1;
	}

	// Token: 0x04001932 RID: 6450
	public float dayCycleLength;

	// Token: 0x04001933 RID: 6451
	public float currentCycleTime;

	// Token: 0x04001934 RID: 6452
	public DayNightController.DayPhase currentPhase;

	// Token: 0x04001935 RID: 6453
	public float hoursPerDay;

	// Token: 0x04001936 RID: 6454
	public float dawnTimeOffset;

	// Token: 0x04001937 RID: 6455
	public int worldTimeHour;

	// Token: 0x04001938 RID: 6456
	public Color fullLight;

	// Token: 0x04001939 RID: 6457
	public Color fullDark;

	// Token: 0x0400193A RID: 6458
	public Material dawnDuskSkybox;

	// Token: 0x0400193B RID: 6459
	public Color dawnDuskFog;

	// Token: 0x0400193C RID: 6460
	public Material daySkybox;

	// Token: 0x0400193D RID: 6461
	public Color dayFog;

	// Token: 0x0400193E RID: 6462
	public Material nightSkybox;

	// Token: 0x0400193F RID: 6463
	public Color nightFog;

	// Token: 0x04001940 RID: 6464
	private float dawnTime;

	// Token: 0x04001941 RID: 6465
	private float dayTime;

	// Token: 0x04001942 RID: 6466
	private float duskTime;

	// Token: 0x04001943 RID: 6467
	private float nightTime;

	// Token: 0x04001944 RID: 6468
	private float quarterDay;

	// Token: 0x04001945 RID: 6469
	private float lightIntensity;

	// Token: 0x0200069D RID: 1693
	public enum DayPhase
	{
		// Token: 0x0400466E RID: 18030
		Night,
		// Token: 0x0400466F RID: 18031
		Dawn,
		// Token: 0x04004670 RID: 18032
		Day,
		// Token: 0x04004671 RID: 18033
		Dusk
	}
}
