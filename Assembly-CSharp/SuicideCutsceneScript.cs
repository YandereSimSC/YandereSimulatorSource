using System;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class SuicideCutsceneScript : MonoBehaviour
{
	// Token: 0x06001BEA RID: 7146 RVA: 0x00146AC4 File Offset: 0x00144CC4
	private void Start()
	{
		this.PointLight.color = new Color(0.1f, 0.1f, 0.1f, 1f);
		this.Door.eulerAngles = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x00146B14 File Offset: 0x00144D14
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 2f)
		{
			this.Speed += Time.deltaTime;
			this.Rotation = Mathf.Lerp(this.Rotation, -45f, Time.deltaTime * this.Speed);
			this.PointLight.color = new Color(0.1f + this.Rotation / -45f * 0.9f, 0.1f + this.Rotation / -45f * 0.9f, 0.1f + this.Rotation / -45f * 0.9f, 1f);
			this.Door.eulerAngles = new Vector3(0f, this.Rotation, 0f);
		}
	}

	// Token: 0x040033CC RID: 13260
	public Light PointLight;

	// Token: 0x040033CD RID: 13261
	public Transform Door;

	// Token: 0x040033CE RID: 13262
	public float Timer;

	// Token: 0x040033CF RID: 13263
	public float Rotation;

	// Token: 0x040033D0 RID: 13264
	public float Speed;
}
