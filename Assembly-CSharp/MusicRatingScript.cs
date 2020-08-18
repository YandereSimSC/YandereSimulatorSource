using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class MusicRatingScript : MonoBehaviour
{
	// Token: 0x060000A6 RID: 166 RVA: 0x0000A0E9 File Offset: 0x000082E9
	private void Start()
	{
		if (this.SFX != null)
		{
			this.SFX.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000A114 File Offset: 0x00008314
	private void Update()
	{
		base.transform.localPosition += new Vector3(0f, this.Speed * Time.deltaTime, 0f);
		base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, new Vector3(0.2f, 0.1f, 0.1f), Time.deltaTime);
		this.Timer += Time.deltaTime * 5f;
		if (this.Timer > 1f)
		{
			this.MyRenderer.material.color = new Color(1f, 1f, 1f, 2f - this.Timer);
			if (this.MyRenderer.material.color.a <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x0400017D RID: 381
	public Renderer MyRenderer;

	// Token: 0x0400017E RID: 382
	public AudioSource SFX;

	// Token: 0x0400017F RID: 383
	public float Speed;

	// Token: 0x04000180 RID: 384
	public float Timer;
}
