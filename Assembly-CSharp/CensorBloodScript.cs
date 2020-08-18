using System;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class CensorBloodScript : MonoBehaviour
{
	// Token: 0x06001223 RID: 4643 RVA: 0x00080324 File Offset: 0x0007E524
	private void Start()
	{
		if (GameGlobals.CensorBlood)
		{
			this.MyParticles.main.startColor = new Color(1f, 1f, 1f, 1f);
			this.MyParticles.colorOverLifetime.enabled = false;
			this.MyParticles.GetComponent<Renderer>().material.mainTexture = this.Flower;
		}
	}

	// Token: 0x0400154B RID: 5451
	public ParticleSystem MyParticles;

	// Token: 0x0400154C RID: 5452
	public Texture Flower;
}
